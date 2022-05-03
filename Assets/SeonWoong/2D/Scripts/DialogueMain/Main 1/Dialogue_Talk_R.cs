using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue_R
{
    public class Dialogue_Talk_R : MonoBehaviour
    {
        public string man_Name   { get; private set; }
        public string woman_Name { get; private set; }

        private Action<Text, string> talk_Action;

        public Dictionary<int, string> name_Order_Dic = null;
        public List<string>            epilogue_Strs  = null;
        public List<string[]>          dialogue_Strs  = null;

        [SerializeField] private Text  speech_Text = null;
        [SerializeField] private Text  name_Text   = null;
        [SerializeField] private Image arrow_Image = null;

        public int epilogueCount = 0;
        public int dialogue_Idx  = 0;
        public int strs_Count    = 0;

        private bool bWait = false;

        private void Awake()
        {
            InitStrs();
        }

        private void Start()
        {
            arrow_Image.DOFade(0.0f, 0.75f)
            .OnComplete(() => arrow_Image.DOFade(1.0f, 0.75f))
            .SetLoops(-1, LoopType.Yoyo);
            arrow_Image.DOPause();

            Epilogue_Talk();
        }

        private void InitStrs() // 대사 받아오기
        {
            epilogue_Strs  = new List<string>(Dialogue_Manager_R.dlg_Strs.epilogueArr);
            dialogue_Strs  = new List<string[]>();

            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_1_Speech);
            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_2_Speech);
            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_3_Speech);
        }

        private void InitNameDic()
        {
            name_Order_Dic = new Dictionary<int, string>();

            name_Order_Dic.Add(0, man_Name);
            name_Order_Dic.Add(1, woman_Name);
            name_Order_Dic.Add(2, man_Name);
            name_Order_Dic.Add(3, man_Name);
            name_Order_Dic.Add(4, woman_Name);
            name_Order_Dic.Add(5, man_Name);
            name_Order_Dic.Add(6, woman_Name);
            name_Order_Dic.Add(7, man_Name);
            name_Order_Dic.Add(8, woman_Name);
        }

        private void Talk(Text _speech_Text, string _str, Action _action = null)
        {
            float _duration = _str.Length * 0.1f;
        
            ClearSpeech(_speech_Text);

            _speech_Text.DOText(_str, _duration)
            .OnComplete(() => 
            {
                bWait = true;

                arrow_Image.DORestart();

                StartCoroutine(WaitTalk(() => 
                {
                    _action?.Invoke();
                }));
            });
        }

        private void Epilogue_Talk()
        {
            if (epilogueCount < epilogue_Strs.Count)
            {
                Talk(speech_Text, epilogue_Strs[epilogueCount], () =>
                {
                    epilogueCount++;
                    Epilogue_Talk();
                });
            }
            else
            {
                Main_Talk(dialogue_Idx);
            }
        }

        public void Main_Talk(int _dialogue_Idx)
        {     
            Talk(speech_Text, dialogue_Strs[_dialogue_Idx][strs_Count], () =>
            {
                strs_Count++;

                if(strs_Count < dialogue_Strs[_dialogue_Idx].Length - 1)
                {
                    Main_Talk(_dialogue_Idx);
                    return;
                }   
            });

            strs_Count = 0;
            dialogue_Idx++;
            Main_Talk(dialogue_Idx);
        }

        private IEnumerator WaitTalk(Action _nextAction = null)
        {
            while (bWait)
            {
                

                Dialogue_Manager_R.Instance.dlg_Ctrl.InputSpeech(ref bWait);

                yield return new WaitUntil(() => true);
            }

            Debug.Log("다음 내용");
            
            NameChange();

            _nextAction?.Invoke();
        }

        public void SetName(string _manName, string _womanName)
        {
            man_Name   = _manName;
            woman_Name = _womanName;

            InitNameDic();
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
