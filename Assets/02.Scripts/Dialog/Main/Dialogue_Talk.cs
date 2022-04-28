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
        public string man_Name   { get; private set; }
        public string woman_Name { get; private set; }

        private Action<Text, string> talk_Action;

        public Dictionary<int, string> name_Order_Dic = new Dictionary<int, string>();
        public List<string>            epilogue_Strs  = new List<string>();
        public List<string[]>          dialogue_Strs  = new List<string[]>();

        [SerializeField] private Text  speech_Text = null;
        [SerializeField] private Text  name_Text   = null;
        [SerializeField] private Image arrow_Image = null;

        public int epilogueCount = 0;

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

            Talk(speech_Text, epilogue_Strs[epilogueCount], 1.0f);
        }

        private void InitStrs() // 대사 받아오기
        {
            epilogue_Strs = Dialogue_Manager.dlg_Strs.epilogueArr.ToList();

            dialogue_Strs.Add(Dialogue_Manager.dlg_Strs.dialogue_1_Speech);
            dialogue_Strs.Add(Dialogue_Manager.dlg_Strs.dialogue_2_Speech);
            dialogue_Strs.Add(Dialogue_Manager.dlg_Strs.dialogue_3_Speech);
        }

        private void InitNameDic()
        {
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

        private void Talk(Text _speech_Text, string _str, float _duration, bool _nextInvoke = true, Action _action = null)
        {
            ClearSpeech(_speech_Text);

            if(epilogueCount < epilogue_Strs.Count - 1)
            name_Text.text = name_Order_Dic[epilogueCount % (epilogue_Strs.Count)];
            //else
            //NameChange();

            _speech_Text.DOText(_str, _duration)
            .OnComplete(() =>
            {
                bWait = true;

                arrow_Image.DORestart();

                if (_nextInvoke)
                {
                    if (epilogueCount < epilogue_Strs.Count - 1
                    && !Dialogue_Manager.Instance.dlg_Option.canSelect)
                    {
                        epilogueCount++;

                        StartCoroutine(WaitTalk(() => Epilogue_Talk()));
                    }
                    else
                    {
                        Dialogue_Manager.Instance.dlg_Option.SetCurOption(0);

                        StartCoroutine(WaitTalk(() =>
                        {
                            Talk(speech_Text, "대화 선택지 중 하나를 골라보자", 0.5f, false, () => 
                            {
                                Dialogue_Manager.Instance.dlg_Option.canSelect = true;
                                name_Text.text = man_Name;
                            });
                        }));
                    
                    }
                }
                
                if(_action != null)
                {
                    StartCoroutine(WaitTalk(() => _action.Invoke()));
                }
            });
        }

        private void Epilogue_Talk()
        {
            Talk(speech_Text, epilogue_Strs[epilogueCount], 0.5f);
        }

        public void Main_Talk(int _talkIndex)
        {
            int strs_Length = dialogue_Strs[_talkIndex].Length;
            int order = 0;

            if (strs_Length == 2)
            {
                Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, false, () =>
                {
                    //NameChange();
                    order++;
                    Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, true, () => 
                    {
                        Dialogue_Manager.Instance.isDoingGame = false;
                    });                   
                });
            }
            else
            {
                Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, false, () =>
                {
                    //NameChange();
                    order++;
                    Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, false, () =>
                    {
                        //NameChange();
                        order++;
                        Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, false, () =>
                        {
                            //NameChange();
                            order++;
                            Talk(speech_Text, dialogue_Strs[_talkIndex][order], 1.0f, false, () =>
                            {
                                //TODO : 화면 페이드 - OnComplete -> LoadScene    
                                Dialogue_Manager.Instance.ChangeView(() => 
                                {
                                    // TODO : 마우스 커서 움직이기
                                    Dialogue_Manager.Instance.FadeDialogue(() => 
                                    {
                                        LoadScene.LoadingScene("TestMap");
                                    });
                                });
                            });
                        });
                    });
                });
            }
        }

        private IEnumerator WaitTalk(Action _nextAction = null)
        {
            while (bWait)
            {
                Dialogue_Manager.Instance.dlg_Ctrl.InputSpeech(ref bWait);

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
