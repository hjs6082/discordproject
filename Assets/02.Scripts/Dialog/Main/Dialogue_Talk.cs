using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue
{
    public class Dialogue_Talk : MonoBehaviour
    {
        private static readonly Dialogue_Strs dlg_Strs = new Dialogue_Strs();
        private Dialogue_Control dlg_Ctrl;

        private Action<Text, string> talk_Action;

        public List<string> manStory_Strs     = new List<string>();
        public List<string> womanStory_Strs   = new List<string>();
        public List<string> man_Strs          = new List<string>();
        public List<string> woman_Strs        = new List<string>();

        [SerializeField] private Text  speech_Text = null;
        [SerializeField] private Text  name_Text   = null;
        [SerializeField] private Image arrow_Image  = null;

        public string man_Name   { get; private set; }
        public string woman_Name { get; private set; }

        private bool bWait = false;

        private void Awake()
        {
            InitStrs();
        }

        private void Start()
        {
            Talk(speech_Text, man_Strs[0], 1.0f);
        }

        private void InitStrs() // 대사 받아오기
        {
            manStory_Strs   = dlg_Strs.manStoryArr.ToList();
            womanStory_Strs = dlg_Strs.womanStoryArr.ToList();
            man_Strs        = dlg_Strs.man_Arr.ToList();
            woman_Strs      = dlg_Strs.woman_Arr.ToList();
        }

        private void Talk(Text _speech_Text, string _str, float _duration)
        {
            ClearSpeech(_speech_Text);
            _speech_Text.DOText(_str, _duration).OnComplete(() => 
            {
                bWait = true;

                arrow_Image.DOFade(0.0f, 0.75f)
                .OnComplete(() => arrow_Image.DOFade(1.0f, 0.75f))
                .SetLoops(-1, LoopType.Yoyo);

                StartCoroutine(WaitTalk());
            });
        }

        private IEnumerator WaitTalk(Action _nextAction = null)
        {
            while(bWait)
            {
                dlg_Ctrl.InputSpeech(ref bWait);

                yield return new WaitForFixedUpdate();
            }

            Debug.Log("다음 내용");
            _nextAction?.Invoke();
        }

        private void RemoveStrs()
        {
            man_Strs.RemoveAt(0);
            woman_Strs.RemoveAt(0);
        }

        public void SetName(string _manName, string _womanName)
        {
            man_Name = _manName;
            woman_Name = _womanName;
        }

        public void NameChange()
        {
            name_Text.text = (name_Text.text == man_Name) ? woman_Name : man_Name;
        }

        public void SkipSpeech(Text _speechText)
        {
            DOTween.Complete(_speechText);
        }

        public void ClearSpeech(Text _speechText)
        {
            _speechText.text = string.Empty;
        }

        public void PlayAudio(AudioClip _audioClip, AudioSource _audioSource)
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClip;
            _audioSource.Play();
        }

        public Text GetSpeechText()
        {
            return speech_Text;
        }
    }
}
