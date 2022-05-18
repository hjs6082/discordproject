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
        public string man_Name { get; private set; }
        public string woman_Name { get; private set; }

        private Action<Text, string> talk_Action;

        public List<string> name_Order_List = null;
        public List<string> epilogue_Strs = null;
        public List<string> pcView_Strs = null;

        [SerializeField] private Text speech_Text = null;
        [SerializeField] private Text name_Text = null;
        [SerializeField] private Image arrow_Image = null;
        [SerializeField] private Text pc_Text = null;
        [SerializeField] private Text pc_Arrow = null;

        public int epilogueCount = 0;
        public int dialogue_Idx = 0;
        public int strs_Count = 0;
        public int pc_TalkVal = 0;

        private bool bWait = false;

        private void Awake()
        {
            InitStrs();


            Cursor.visible = false;
        }

        private void Start()
        {
            arrow_Image.DOFade(0.0f, 0.75f)
            .OnComplete(() => arrow_Image.DOFade(1.0f, 0.75f))
            .SetLoops(-1, LoopType.Yoyo);
            arrow_Image.DOPause();

            pc_Arrow.gameObject.SetActive(false);

            Epilogue_Talk();
        }

        private void InitStrs() // 대사 받아오기
        {
            epilogue_Strs = new List<string>(Dialogue_Strs.GetStrsToList(Dialogue_Strs.EPILOGUE_STRS));
            pcView_Strs = new List<string>(Dialogue_Strs.GetStrsToList(Dialogue_Strs.PC_VIEW_STRS));
        }

        private void InitName(List<string> _strs)
        {
            name_Order_List.Clear();

            for (int i = 0; i < _strs.Count; i++)
            {
                string[] nameMark = _strs[i].Split('-');
                string name = (nameMark[1] == "m") ? man_Name : woman_Name;

                name_Order_List.Add(name);
            }
        }

        private void Talk(Text _speech_Text, int _idx, string _str, Action _action = null, Action _arrow_Event = null)
        {
            float _duration = _str.Length * 0.075f;

            ClearSpeech(_speech_Text);
            StartCoroutine(FastTalk());

            _speech_Text.DOText(_str.Split('-')[0], _duration)
            .OnComplete(() =>
            {
                bWait = true;

                _arrow_Event?.Invoke();

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
                }, () => arrow_Image.DORestart());
            }
            else
            {
                speech_Text = pc_Text;

                InitName(pcView_Strs);

                Dialogue_Manager_R.Instance.ChangeView(() =>
                {
                    PC_View_Talk();
                });
            }
        }

        public void PC_View_Talk()
        {
            Time.timeScale = 1.0f;
            pc_Arrow.gameObject.SetActive(false);
            Talk(speech_Text, pc_TalkVal, pcView_Strs[pc_TalkVal], () =>
            {
                pc_TalkVal++;
                if (pc_TalkVal <= pcView_Strs.Count)
                {
                    switch (pc_TalkVal)
                    {
                        case 0:
                        case 1:
                            {
                                PC_View_Talk();
                            }
                            break;
                        case 2:
                        case 3:
                        case 4:
                            {
                                PC_View_Talk();
                                Download();
                            }
                            break;
                        case 5:
                            {
                                GameManager.Instance.Fade_OutIn(0.5f, () =>
                                {

                                    Dialogue_Manager_R.Instance.Get_PC_View().localScale = new Vector3(1.0f, 1.0f, 1.0f);
                                    PC_View_Talk();
                                    Dialogue_Manager_R.Instance.game_Info_Text.text = string.Empty;
                                    Dialogue_Manager_R.Instance.game_Info_Text.DOText(Dialogue_Strs.GAME_INFO_STRS[1], 3.0f, true, ScrambleMode.Numerals)
                                    .SetDelay(0.25f);
                                    // TODO : 큰 소리 효과음, 쿵 하는 소리
                                    // PlayAudio(GameManager.Instance.explosion_AD, GameManager.Instance.effectAudio, () => 
                                    // {


                                    //     return;
                                    // });
                                });
                            }
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            {
                                PC_View_Talk();
                            }
                            break;
                        case 11:
                            {
                                PC_View_Talk();
                                Download();
                                // TODO : 마우스 이동
                            }
                            break;
                        case 12:
                            {
                                Dialogue_Manager_R.Instance.dlg_Cursor.MoveCursor(() =>
                                {
                                    GameManager.Instance.Fade_Out(0.5f, () =>
                                    {
                                        Time.timeScale = 1.0f;
                                        Cursor.visible = true;
                                        LoadScene.LoadingScene("MapJunseo");
                                    }, Ease.Linear, Color.white);
                                });
                            }
                            break;
                    }
                }
            }, () => pc_Arrow.gameObject.SetActive(true));
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

        private IEnumerator FastTalk()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    break;
                }

                yield return new WaitUntil(() => true);
            }

            Time.timeScale = 5.0f;
        }

        public void Download()
        {
            Sequence seq = DOTween.Sequence();

            float curAmount = Dialogue_Manager_R.Instance.download_Guage.fillAmount;

            seq.Append(
                Dialogue_Manager_R.Instance.download_Guage.DOFillAmount(curAmount + 0.1f, 0.5f)
                .SetEase(Ease.OutQuad)
            );

            Vector3 curScale = Dialogue_Manager_R.Instance.Get_PC_View().localScale;
            Vector3 scaleOffset = new Vector3(0.1f, 0.1f, 0.0f);

            seq.Join(
                Dialogue_Manager_R.Instance.Get_PC_View().DOScale(curScale + scaleOffset, 0.2f)
                .SetEase(Ease.OutCubic)
            );
        }

        public void SetName(string _manName, string _womanName)
        {
            man_Name = _manName;
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

        public void PlayAudio(AudioClip _audioClip, AudioSource _audioSource, Action _callback = null)
        {
            _audioSource.Stop();
            _audioSource.clip = _audioClip;
            _audioSource.Play();

            StartCoroutine(Delay(_audioClip.length, _callback));
        }

        IEnumerator Delay(float _duration, Action _callback)
        {
            yield return new WaitForSeconds(_duration);

            _callback?.Invoke();
        }

        public Text GetSpeechText()
        {
            return speech_Text;
        }
    }
}
