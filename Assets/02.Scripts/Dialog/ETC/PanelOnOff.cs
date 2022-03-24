using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelOnOff : MonoBehaviour
{
    public Vector3 on_Pos;
    public Vector3 off_Pos;

    public GameObject[] miniGames;

    private RectTransform rectTrm = null;

    bool isOn = true;
    bool isAttack = false;

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

        rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutCirc).OnComplete(() => 
        {
            if(isAttack)
            {
                isAttack = false;
                Dialog.DialogManager.Instance.dialog_Ctrl.Talking(1.0f, "");
            }
        });
    }

    public void OnOff(GameObject _miniGame = null)
    {
        isOn = !isOn;

        Vector3 pos = (isOn) ? on_Pos : off_Pos;

        rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            InitMiniGames();

            if (_miniGame != null)
            {
                _miniGame.SetActive(true);

                Dialog.DialogManager.Instance.OnOffButtons(false);
            }
            else
            {
                isAttack = true;
            }

            OnOff();
        });
    }

    public void MiniGame(int _index)
    {
        GameObject miniGame = miniGames[_index];

        OnOff(miniGame);
    }
}
