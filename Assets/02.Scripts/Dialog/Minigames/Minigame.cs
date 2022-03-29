using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    public abstract void Awake();
    public abstract void Start();
    public abstract void Update();
    public abstract void InitGame();

    public virtual void Attack(bool _bWin)
    {
        Dialog.Dialog_Manager.damaged?.Invoke(_bWin);
    }

    public virtual void StartGame(GameObject _readyPanel)
    {
        _readyPanel.SetActive(false);
    }
}
