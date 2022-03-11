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
    public abstract class DialogControl_main_Dot : MonoBehaviour
    {
        public abstract void Awake();
        public abstract void Next(AudioSource _audioSource);
        public abstract void Talking(float duration, string _str); // 대화
        public abstract void SkipSpeech();


        public virtual void Damaged()
        {
            RectTransform rect = GetComponent<RectTransform>();

            rect.DOShakeAnchorPos(0.1f);
        }

        public virtual void ClearSpeech(Text _speechText)
        {
            _speechText.text = "";
        }

        public virtual void PlayAudio(AudioClip _audioClip, AudioSource _audioSource)
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }
    }
}
