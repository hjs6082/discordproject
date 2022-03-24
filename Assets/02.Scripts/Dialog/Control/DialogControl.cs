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
    public class DialogControl : MonoBehaviour
    {
        public Text speechText = null;
        public Text nameText = null;

        public void Awake()
        {

        }
        public void Start()
        {

        }
        public void Next(AudioSource _audioSource = null)
        {

        }

        public void Talking(float duration, string _str) // 대화
        {
            ClearSpeech();
            speechText.DOText("(이것은 공격 대사입니다.\n이것은 공격 대사입니다.)", duration).OnComplete(() =>
            {
                Dialog.DialogManager.Instance.AddHeart();
            });
        }

        public void SkipSpeech()
        {

        }


        public void ClearSpeech()
        {
            speechText.text = "";
        }

        public void PlayAudio(AudioClip _audioClip, AudioSource _audioSource)
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
}
