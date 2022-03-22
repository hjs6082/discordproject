using UnityEngine;
using UnityEngine.UI;

public class Arrow_Manager : Minigame
{
    private const float DEFAULT_TIME = 8.0f;

    public Arrow_Control arrow_Ctrl { get; private set; }
    private Arrow_Obj arrow_Obj = null;

    public GameObject readyPanel = null;

    public Text timer_Text = null;
    private float currentTime = 0.0f;

    private bool bStart = false;
    private bool bWin = false;

    public override void Awake()
    {
        arrow_Ctrl = GetComponentInChildren<Arrow_Control>();
    }

    public override void Start()
    {
        arrow_Obj = arrow_Ctrl.arrow_Obj;

        InitGame();
    }

    public override void Update()
    {
        if(!bStart)
        {
            StartGame(readyPanel);
        }   
        else
        {
            Timer();
            InputArrow();
        }
    }

    public override void InitGame()
    {
        bStart = false;

        currentTime = DEFAULT_TIME;

        readyPanel.SetActive(true);

        arrow_Obj.InitArrow();
    }

    public override void StartGame(GameObject _readyPanel)
    {
        if(Input.anyKeyDown)
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

        if(bWin || currentTime <= 0.0f)
        {
            Debug.Log("dmddo");
            Attack(bWin);
        }
    }

    public void Timer()
    {
        currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0.0f, DEFAULT_TIME);

        timer_Text.text = $"{currentTime:F2}";
    }
}