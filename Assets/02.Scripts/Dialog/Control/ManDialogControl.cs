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

        public Text speechText;

        public override void Awake()
        {

        }

        public override void Start()
        {
            throw new NotImplementedException();
        }

        public override void Next(AudioSource _audioSource)
        {
            
        }

        public override void Talking(float duration, string _str) // 대화
        {
            
        }

        public override void Damaged()
        {
            
        }

        public override void SkipSpeech()
        {
            DOTween.Complete(this.speechText);
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
