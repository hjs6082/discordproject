using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Arrow_Manager : Minigame
{
    private const float DEFAULT_TIME = 4.25f;
    public static Action Act_Timer;

    public Arrow_Control arrow_Ctrl { get; private set; }
    private Arrow_Obj arrow_Obj = null;

    public GameObject readyPanel = null;

    public Text timer_Text = null;
    public Text dec_Timer_Text = null;
    private float currentTime = 0.0f;

    private bool bStart = false;
    private bool bWin = false;

    public override void Awake()
    {
        arrow_Ctrl = GetComponentInChildren<Arrow_Control>();

        Act_Timer += WrongArrow;

        dec_Timer_Text.gameObject.SetActive(false);
    }

    public override void Start()
    {
        arrow_Obj = arrow_Ctrl.arrow_Obj;

        InitGame();
    }

    public override void Update()
    {
        if (bStart)
        {
            Timer();
            InputArrow();
        }
        else
        {
            StartGame(readyPanel);
        }
    }

    public override void InitGame()
    {
        bStart = false;

        InitTimer();

        readyPanel.SetActive(true);

        arrow_Obj?.InitArrow();
    }

    public override void StartGame(GameObject _readyPanel)
    {
        if (Input.anyKeyDown && _readyPanel.activeSelf)
        {
            base.StartGame(_readyPanel);
            bStart = true;
        }
    }

    public override void Attack(bool _bWin)
    {
        base.Attack(_bWin);
    }

    public void InputArrow()
    {
        bWin = arrow_Ctrl.InputArrow();

        if (bWin || currentTime <= 0.0f)
        {
            Debug.Log("dmddo");
            Attack(bWin);
            bStart = false;
        }
    }

    public void UpdateTimer(Action _Act_Timer)
    {
        _Act_Timer?.Invoke();
        currentTime = Mathf.Clamp(currentTime, 0.0f, DEFAULT_TIME);
        timer_Text.text = $"{currentTime:F2}";
    }

    public void WrongArrow()
    {
        UpdateTimer(() => DecTime());
    }

    public void InitTimer()
    {
        UpdateTimer(() => currentTime = DEFAULT_TIME);
    }

    public void Timer()
    {
        UpdateTimer(() => currentTime -= Time.deltaTime);
    }

    public void DecTime()
    {
        currentTime -= 0.1f;

        dec_Timer_Text.DOComplete();
        dec_Timer_Text.gameObject.SetActive(true);
        dec_Timer_Text.rectTransform.DOShakeAnchorPos(0.25f, 10, 15).OnComplete(() => 
        {
            dec_Timer_Text.gameObject.SetActive(false);
        });
    }
}