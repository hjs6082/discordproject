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

        public void OnOff(bool _bWin, bool _isAttack = false)
        {
            isOn = !isOn;

            Vector3 pos = (isOn) ? on_Pos : off_Pos;

            rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutCirc).OnComplete(() =>
            {
                if(_isAttack)
                {
                    Dialog_Talk dialog_Talk = Dialog_Manager.Instance.dialog_Talk;

                    dialog_Talk.Attack_Talk(_bWin, 1.25f, 1.0f, () => 
                    {
                        Dialog_Manager.Instance.AddHeart();
                        if(dialog_Talk.speech_Index >= 2)
                        {
                            dialog_Talk.Attack_Talk(_bWin, 0.75f, 1.5f, () =>
                            {
                                /// 씬이동
                            });
                        }
                    });
                }
            });
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
