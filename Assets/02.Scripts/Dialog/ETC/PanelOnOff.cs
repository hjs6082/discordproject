using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialog
{
    public class PanelOnOff : MonoBehaviour
    {
        public Vector3 on_Pos;
        public Vector3 off_Pos;

        public GameObject[] miniGames;

        private RectTransform rectTrm = null;
        public ParticleSystem paper_Explosion = null;
        public GameObject inGameImage = null;

        bool isOn = true;

        private void Awake()
        {
            rectTrm = GetComponent<RectTransform>();
        }

        private void Start()
        {
            InitMiniGames();
        }

        private void InitMiniGames()
        {
            for (int i = 0; i < miniGames.Length; i++)
            {
                miniGames[i].GetComponent<Arrow_Manager>().InitGame();
                miniGames[i].SetActive(false);
            }
        }

        public void OnOff()
        {
            isOn = !isOn;

            Vector3 pos = (isOn) ? on_Pos : off_Pos;

            rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutFlash);
        }

        public void OnOff(bool _bWin, bool _isAttack = false)
        {
            isOn = !isOn;

            Vector3 pos = (isOn) ? on_Pos : off_Pos;

            rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutFlash).OnComplete(() =>
            {
                if(_isAttack)
                {
                    Dialog_Talk dialog_Talk = Dialog_Manager.Instance.dialog_Talk;

                    dialog_Talk.Attack_Talk(_bWin, 1.25f, 1.0f, () => 
                    {
                        if(dialog_Talk.speech_Index < 2)
                        {
                            /// AddHeart

                            Dialog_Manager.Instance.AddHeart();
                        }
                        else
                        {
                            /// Attack_Talk
                            Dialog_Manager.Instance.AddHeart();
                            StartCoroutine(Delay(0.25f, () =>
                            {
                                paper_Explosion.Play();
                                dialog_Talk.ClearSpeech(dialog_Talk.GetSpeechText());
                            }));
                            
                            StartCoroutine(Delay(paper_Explosion.main.duration / 1.3f, () => 
                            {
                                dialog_Talk.Attack_Talk(_bWin, 0.75f, 1.5f, () =>
                                {
                                /// 씬이동
                                    GameManager.Instance.Fade_Out(0.25f, () => 
                                    {
                                        LoadScene.LoadingScene("MoveScene");
                                    });

                                });
                            }));
                            
                        }
                    });
                }
            });
        }

        IEnumerator Delay(float delay, Action _action)
        {
            yield return new WaitForSeconds(delay);
            _action?.Invoke();
        }

        public void OnOff(bool _bWin, GameObject _miniGame = null)
        {
            bool isAttack = (_miniGame == null) ? true : false;

            isOn = !isOn;

            Vector3 pos = (isOn) ? on_Pos : off_Pos;

            rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                InitMiniGames();

                if (_miniGame != null)
                {
                    _miniGame.SetActive(true);

                    Dialog.Dialog_Manager.Instance.OnOffButtons(false);
                }

                OnOff(_bWin, isAttack);
            });
        }

        public void MiniGame(int _index)
        {
            GameObject miniGame = miniGames[_index];

            OnOff(false, miniGame);
        }
    }
}
