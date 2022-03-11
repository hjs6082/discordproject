using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Dialog
{
    public class ManDialogControl : DialogControl_main
    {
        private const string ATTACK_PATH = "Voices/Voice_0/audio_";
        private const string STORY_PATH = "Voices/Voice_3/audio_";

        private int talkVal = 0;

        public List<string> attackStrList = new List<string>();
        public List<string> storyStrList = new List<string>();

        public List<AudioClip> attackClipList = new List<AudioClip>();
        public List<AudioClip> storyClipList = new List<AudioClip>();

        public GameObject charImage;
        private AttackTween attackTween;
        private CharacterAnimation charAnim;

        public Image goodGuage;
        public Image badGuage;

        public GameObject arrowText;
        public Text speetchText;

        public bool isPlayer = false;

        public override void Awake()
        {
#region 변수 초기화
            attackTween = charImage.GetComponent<AttackTween>();
            charAnim = GetComponentInChildren<CharacterAnimation>();

            attackStrList = DialogStrs.manStrsArr.ToList();
            storyStrList = DialogStrs.manStoryArr.ToList();

            for(int i = 0; i < attackStrList.Count; i++)
            {
                AudioClip clip = Resources.Load<AudioClip>(ATTACK_PATH + i);
                attackClipList.Add(clip);
            }
            for(int i = 0; i < storyStrList.Count; i++)
            {
                AudioClip clip = Resources.Load<AudioClip>(STORY_PATH + i);
                storyClipList.Add(clip);
            }   
#endregion

#region 오브젝트 초기화
            goodGuage.fillAmount = 0.0f;
            badGuage.fillAmount = 0.0f;

            speetchText.text = "";

            arrowText.SetActive(false);
#endregion
        }

        public void Attack(int _attackVal, AudioSource _audioSource)
        {
            AudioClip clip = attackClipList[_attackVal];

            float duration = clip.length;
         
            arrowText.SetActive(false);

            charAnim.Attack(_attackVal);

            PlayAudio(clip, _audioSource);

            Talking(duration, attackStrList[_attackVal]);
        }

        public override void Next(AudioSource _audioSource)
        {
            AudioClip clip = storyClipList[talkVal];

            float duration = clip.length;

            arrowText.SetActive(false);

            ClearSpeech(speetchText);

            PlayAudio(clip, _audioSource);
            Talking(duration, storyStrList[talkVal]);

            talkVal++;
        }

        public override void Talking(float duration, string _str) // 대화
        {
            ClearSpeech(speetchText);

            speetchText.DOText(_str, duration)
            .OnComplete(() => {
                arrowText.SetActive(true);
                DialogManager.Instance.isTalking = false;
                DialogManager.Instance.NextOrder();

                if(isPlayer)
                {
                    attackTween.Attack();
                    DialogManager.Instance.womanCtrl.Damaged();
                    isPlayer = false;
                }
            });
        }

        public override void Damaged()
        {
            Image guage;

            float guageAmount;
            int randomGuage = UnityEngine.Random.Range(0, 2);
            float randomDamage = UnityEngine.Random.Range(5f, 30f);

            base.Damaged();

            switch(randomGuage)
            {
                case 0:
                guage = goodGuage;
                break;
                case 1:
                guage = badGuage;
                break;
                default:
                guage = goodGuage;
                break;
            }

            
            guageAmount = guage.fillAmount;
            guage.DOFillAmount(guageAmount + randomDamage / 100f, 0.5f);
        }

        public override void SkipSpeech()
        {
            DOTween.Complete(this.speetchText);
        }

        public override void ClearSpeech(Text _speechText)
        {
            base.ClearSpeech(_speechText);
        }

        public override void PlayAudio(AudioClip _audioClip, AudioSource _audioSource)
        {
            base.PlayAudio(_audioClip, _audioSource);
        }
    }
}
