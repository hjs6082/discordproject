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

        public List<string>            name_Order_List = null;
        public List<string>            epilogue_Strs   = null;
        public List<string[]>          dialogue_Strs   = null;
        public List<string>            pcView_Strs     = null;

        [SerializeField] private Text  speech_Text = null;
        [SerializeField] private Text  name_Text   = null;
        [SerializeField] private Image arrow_Image = null;
        [SerializeField] private Text  pc_Text     = null;
        [SerializeField] private Image pc_Arrow    = null;

        public int epilogueCount = 0;
        public int dialogue_Idx  = 0;
        public int strs_Count    = 0;
        public int pc_TalkVal    = 0;

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
            pcView_Strs    = new List<string>(Dialogue_Manager_R.dlg_Strs.PC_View_Arr);

            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_1_Speech);
            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_2_Speech);
            dialogue_Strs.Add(Dialogue_Manager_R.dlg_Strs.dialogue_3_Speech);
        }

        private void InitName(List<string> _strs)
        {
            name_Order_List.Clear();

            for(int i = 0; i < _strs.Count; i++)
            {
                string[] nameMark = _strs[i].Split('-');
                string name = (nameMark[1] == "m") ? man_Name : woman_Name;

                name_Order_List.Add(name);
            }
        }

        private void Talk(Text _speech_Text, int _idx,string _str, Action _action = null)
        {
            float _duration = _str.Length * 0.075f;
        
            ClearSpeech(_speech_Text);

            _speech_Text.DOText(_str.Split('-')[0], _duration)
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
                NameChange(epilogueCount);
                Talk(speech_Text, epilogueCount, epilogue_Strs[epilogueCount], () =>
                {
                    epilogueCount++;
                    Epilogue_Talk();
                });
            }
            else
            {
                InitName(dialogue_Strs[dialogue_Idx].ToList());
                Main_Talk(dialogue_Idx);
            }
        }

        public void Main_Talk(int _dialogue_Idx)
        {     
            NameChange(strs_Count);

            Talk(speech_Text, strs_Count,dialogue_Strs[_dialogue_Idx][strs_Count], () =>
            {
                strs_Count++;
                Debug.Log(dialogue_Strs[_dialogue_Idx].Length);
                if(strs_Count < dialogue_Strs[_dialogue_Idx].Length)
                {
                    Main_Talk(_dialogue_Idx);
                    return;
                }  
                else
                {
                    strs_Count = 0;

                    dialogue_Idx++;
                    if(dialogue_Idx < dialogue_Strs.Count)
                    {
                        InitName(dialogue_Strs[dialogue_Idx].ToList());
                        Main_Talk(dialogue_Idx);
                    }
                    else
                    {
                        arrow_Image = pc_Arrow;
                        speech_Text = pc_Text;

                        Dialogue_Manager_R.Instance.ChangeView(() => 
                        {
                            PC_View_Talk();
                        });
                    }
                }
            });
        }

        public void PC_View_Talk()
        {
            Talk(speech_Text, pc_TalkVal, pcView_Strs[pc_TalkVal], () => 
            {
                pc_TalkVal++;
                StartCoroutine(WaitTalk(() => 
                {
                    // TODO : 줌

                    speech_Text.transform.parent.gameObject.SetActive(false);
                    
                }));
            });
        }

        private IEnumerator WaitTalk(Action _nextAction = null)
        {
            while (bWait)
            {
                Dialogue_Manager_R.Instance.dlg_Ctrl.InputSpeech(ref bWait);

                yield return new WaitUntil(() => true);
            }

            Debug.Log("다음 내용");

            
            _nextAction?.Invoke();
        }

        public void SetName(string _manName, string _womanName)
        {
            man_Name   = _manName;
            woman_Name = _womanName;

            InitName(epilogue_Strs);
        }

        public void NameChange(int _idx)
        {
            name_Text.DOText(name_Order_List[_idx], 0.15f);
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
