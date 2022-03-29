using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBang_Manager : Minigame
{
    private const int DEFAULT_LIFE = 5;

    private BingBang_Control bingBang_Ctrl = null;
    private BingBang_Obj bingBang_Obj = null;
    [SerializeField] private GameObject readyPanel = null;

    private int currentLife = 0;

    private bool bStart = false;

    public override void Awake()
    {
        bingBang_Ctrl = GetComponentInChildren<BingBang_Control>();

        bingBang_Obj = bingBang_Ctrl.bingBang_Obj;
    }

    public override void Start()
    {
        InitGame();
    }

    public override void Update()
    {
        if(bStart)
        {
            bool bSuccess = bingBang_Ctrl.TwinkleTurn();

            if(!bSuccess) LifeDown();
        }
        else
        {
            StartGame(readyPanel);
        }
    }

    public override void InitGame()
    {
        InitValue();
        bingBang_Obj.RandomTwinkle();
    }

    private void InitValue()
    {
        currentLife = DEFAULT_LIFE;
    }
    
    private void LifeDown()
    {
        currentLife--;
    }

    public override void StartGame(GameObject _readyPanel)
    {
        base.StartGame(_readyPanel);
    }

    public override void Attack(bool _bWin)
    {
        base.Attack(_bWin);
    }
}
