using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialog
{
    public class Dialog_Talk : MonoBehaviour
    {
        private Action<Text, string> talk_Action;

        public List<string> manStory_Strs     = new List<string>();
        public List<string> womanStory_Strs   = new List<string>();
        public List<string> manSuccess_Strs   = new List<string>();
        public List<string> womanSuccess_Strs = new List<string>();
        public List<string> manFail_Strs      = new List<string>();
        public List<string> womanFail_Strs    = new List<string>();
        public List<string> space_Strs        = new List<string>();

        [SerializeField] private Text space_Text      = null;
        [SerializeField] private Text space_Name_Text = null;
        [SerializeField] private Text speech_Text     = null;
        [SerializeField] private Text name_Text       = null;

        public string man_Name   { get; private set; }
        public string woman_Name { get; private set; }

        public int story_Index  = 0; /// 0 : 남자 /// 1 : 여자
        public int speech_Index = 0; /// 0 : 남자 /// 1 : 여자
        public int space_Index  = 0;

        private void Awake()
        {
            InitStrs();
        }

        private void Start()
        {
            space_Name_Text.text = man_Name;

            ClearSpeech(speech_Text);
        }

        private void InitStrs()
        {
            manStory_Strs = Dialog_Strs.manStoryArr.ToList();
            womanStory_Strs = Dialog_Strs.womanStoryArr.ToList();
            manSuccess_Strs = Dialog_Strs.manSuccessArr.ToList();
            womanSuccess_Strs = Dialog_Strs.womanSuccessArr.ToList();
            manFail_Strs = Dialog_Strs.manFailArr.ToList();
            womanFail_Strs = Dialog_Strs.womanFailArr.ToList();
            space_Strs = Dialog_Strs.spaceStrsArr.ToList();
        }

        public void Talk(Text _text, string _str, float duration, float delay, Action _action = null)
        {
            string str = _str;

            _text.DOText(str, duration).SetDelay(delay).OnComplete(() => 
            {
                _action?.Invoke();

                if(_action == null)
                {
                    story_Index++;
                }
            });
        }

        public void Story_Talk()
        {
            NameChange();
            Talk(speech_Text, manStory_Strs[story_Index], 1.0f, 0.5f, () => 
            {
                NameChange();
                ClearSpeech(speech_Text);
                Talk(speech_Text, womanStory_Strs[story_Index], 1.0f, 0.75f, () => 
                {
                    Space_Talk(1.5f, () => 
                    {
                        Dialog_Manager.Instance.OnOffButtons(true);
                    });
                });
            });
        }

        public void Attack_Talk(bool _bWin, float m_Dur, float w_Dur, Action _last = null)
        {
            NameChange();
            
            if(_bWin)
            {
                Talk(speech_Text, manSuccess_Strs[0], m_Dur, 0.25f, () => 
                {
                    NameChange();
                    ClearSpeech(speech_Text);
                    Talk(speech_Text, womanSuccess_Strs[0], w_Dur, 0.25f, () => 
                    {
                        //ClearSpeech(speech_Text);
                        Dialog_Manager.Instance.OnOffButtons(true);

                        RemoveStrs();

                       _last?.Invoke();   

                        speech_Index++;
                    });
                });
            }
            else
            {
                Talk(speech_Text, manFail_Strs[0], m_Dur, 0.25f, () => 
                {
                    NameChange();
                    ClearSpeech(speech_Text);
                    Talk(speech_Text, womanFail_Strs[0], w_Dur, 0.25f, () => 
                    {
                        Dialog_Manager.Instance.OnOffButtons(true);

                        RemoveStrs();

                        _last?.Invoke();

                        speech_Index++;
                    });
                });
            }
        }

        private void RemoveStrs()
        {
            manSuccess_Strs.RemoveAt(0);
            womanSuccess_Strs.RemoveAt(0);
            manFail_Strs.RemoveAt(0);
            womanFail_Strs.RemoveAt(0);
        }

        public void Space_Talk(float _duration, Action _onButton)
        {
            ClearSpeech(space_Text);
            Talk(space_Text, space_Strs[0], _duration, 0.75f, () => 
            {
                _onButton?.Invoke();
                space_Strs.RemoveAt(0);
            });
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
            _speechText.text = "";
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
