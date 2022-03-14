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
    public class FairyDialogControl_Dot : DialogControl_main_Dot
    {
        private const string AUDIO_PATH = "Voices/Voice_2/audio_";

        public int talkVal = 0;

        public List<string> dialogStrList = new List<string>();
        public List<AudioClip> dialogClipList = new List<AudioClip>();

        public GameObject charImage;
        public Text nameText;

        public GameObject arrowText;
        public Text speetchText;

        public override void Awake()
        {
            dialogStrList = DialogStrs.fairyStrsArr.ToList();

            for (int i = 0; i < dialogStrList.Count; i++)
            {
                AudioClip clip = Resources.Load<AudioClip>(AUDIO_PATH + i);

                dialogClipList.Add(clip);
            }

            speetchText.text = "";

            arrowText.SetActive(false);
        }

        private void Start()
        {
            charImage.GetComponent<Image>().color = DialogManager.Instance.SetColor(35f, 35f, 35f, 255f);
        }

        public void Attack()
        {
            
        }

        public override void Next(AudioSource _audioSource)
        {
            if (talkVal != 0)
            {
                charImage.GetComponent<Image>().color = Color.white;
                nameText.text = "요정";
            }
            else if (talkVal == 5)
            {
                
            }
            else if(talkVal == dialogStrList.Count - 1)
            {
                DialogManager.Instance.gameObject.SetActive(false);
                return;
            }

            AudioClip clip = dialogClipList[talkVal];
            float duration = 0;

            if(clip != null)
            {
                duration = clip.length;
            }

            arrowText.SetActive(false);

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
                talkVal++;
            });
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
