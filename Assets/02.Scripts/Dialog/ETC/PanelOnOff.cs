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

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();

        for(int i = 0; i < miniGames.Length; i++)
        {
            miniGames[i].SetActive(false);
        }
    }

    public void OnOff()
    {
        isOn = !isOn;

        Vector3 pos = (isOn) ? on_Pos : off_Pos;

        rectTrm.DOAnchorPos(pos, 0.75f).SetEase(Ease.Linear);
    }

    public void OnOff(GameObject _miniGame)
    {
        _miniGame.SetActive(true);
    }

    public void MiniGame(int _index)
    {
        GameObject miniGame = miniGames[_index];

        OnOff(miniGame);
    }
}
