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
    public class WomanDialogControl_Dot : DialogControl_main_Dot
    {
        private const string AUDIO_PATH = "Voices/Voice_1/audio_";

        private int talkVal = 0;

        public List<string> dialogStrList = new List<string>();

        public List<AudioClip> dialogClipList = new List<AudioClip>();

        public GameObject charImage;
        private AttackTween attackTween;

        public Image goodGuage;
        public Image badGuage;

        public GameObject arrowText;
        public Text speetchText;

        public Animator animator;

        public override void Awake()
        {
            attackTween = charImage.GetComponent<AttackTween>();

            dialogStrList = DialogStrs.womanStrsArr.ToList();

            for (int i = 0; i < dialogStrList.Count; i++)
            {
                AudioClip clip = Resources.Load<AudioClip>(AUDIO_PATH + i);

                dialogClipList.Add(clip);
            }

            goodGuage.fillAmount = 0.0f;
            badGuage.fillAmount = 0.0f;

            speetchText.text = "";

            arrowText.SetActive(false);
        }

        public override void Next(AudioSource _audioSource)
        {
            arrowText.SetActive(false);

            AudioClip clip = dialogClipList[talkVal];

            float duration = clip.length;

            PlayAudio(clip, _audioSource);
            Talking(duration, dialogStrList[talkVal]);
        }

        public override void Talking(float duration, string _str) // 대화
        {
            ClearSpeech(speetchText);

            speetchText.DOText(_str, duration)
            .OnComplete(() =>
            {
                arrowText.SetActive(true);
                DialogManager.Instance.isTalking = false;
                DialogManager.Instance.NextOrder();
                attackTween.Attack();
                DialogManager.Instance.manCtrl.Damaged();
                talkVal++;
            });
        }

        public override void Damaged()
        {
            Image guage;

            float guageAmount;
            int randomGuage = UnityEngine.Random.Range(0, 2);
            float randomDamage = UnityEngine.Random.Range(5f, 30f);

            base.Damaged();

            switch (randomGuage)
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

        public override void PlayAudio(AudioClip _audioClip, AudioSource _audioSource)
        {
            base.PlayAudio(_audioClip, _audioSource);
        }
    }
}
