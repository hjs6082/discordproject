using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public enum eScene
    {
        MAIN,
        DIALOG,
        GAME3D
    };

    const string fileName = "PuzzleSave";

    public static GameManager Instance { get; private set; }

    public string ManName = "";
    public string WomanName = "";

    public GameObject FadePanel;
    public bool isOnLoad;

    public GameObject PauseCanvas;
    public GameObject PausePanel;
    public GameObject OptionPanel;
    public GameObject CreditPanel;
    public GameObject OptionBGM_Volume;
    public GameObject OptionEFFECT_Volume;
    public bool bPause = false;
    public bool isPuzzle = false;

    public AudioClip[] BGM_Arr;
    private AudioSource bgmAudio;
    private AudioManager audioManager;

    public bool bChessClear = false;
    public bool bMovePuzzleClear = false;
    public bool bOnIsland = false;

    public bool stage1Start = false;

    //public Vector3 curPlayerPos;

    public GameObject ClearPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        PauseCanvas.SetActive(false);
        ClearPanel.SetActive(false);

        //curPlayerPos = new Vector3(-25f, 16f, 10f);
        FadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        FadePanel.SetActive(false);

        SaveData savePuzzle = SaveSystem.Load(fileName);

        if (savePuzzle != null)
        {
            bChessClear = savePuzzle.bChessClear;
            bMovePuzzleClear = savePuzzle.bMovePuzzleClear;
            bOnIsland = savePuzzle.bOnIsland;
        }

        audioManager = GetComponentInChildren<AudioManager>();

        Debug.Log(bChessClear);
    }

    private void Start()
    {
        ChangeBGM(eScene.MAIN);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPause();
            }
            if (bPause)
            {
                audioManager.BGM_Source.Pause();
                audioManager.EFFECT_Source.Pause();

                if(CharacterVoice.Instance != null)
                CharacterVoice.Instance.audioSource.Pause();
            }
            else
            {
                audioManager.BGM_Source.UnPause();
                audioManager.EFFECT_Source.UnPause();

                if(CharacterVoice.Instance != null)
                CharacterVoice.Instance.audioSource.UnPause();
            }
         
            Time.timeScale = bPause ? 0f : 1f;
        }
    }

    public bool DemoClearCheck()
    {
        if(bChessClear && bMovePuzzleClear)
        {
            return true;
        }
        return false;
    }

    public void SavePuzzle()
    {
        SaveData savePuzzle = new SaveData(bChessClear, bMovePuzzleClear, bOnIsland);
        SaveSystem.Save(savePuzzle, fileName);
    }

    public void ResetVolumeController()
    {
        audioManager.BGM_Volume = OptionBGM_Volume;
        audioManager.EFFECT_Volume = OptionEFFECT_Volume;

        audioManager.InitVolumeSettings();
    }

    public void OnPause()
    {
        bPause = !bPause;
        PauseCanvas.SetActive(bPause);
        InitPanel();
        PausePanel.SetActive(bPause);
    }

    private void InitPanel()
    {
        PausePanel.SetActive(false);
        OptionPanel.SetActive(false);
        CreditPanel.SetActive(false);
    }

    public void Pause()
    {
        InitPanel();
        PausePanel.SetActive(true);
    }

    public void Option()
    {
        InitPanel();
        OptionPanel.SetActive(true);
    }

    public void Credit()
    {
        InitPanel();
        CreditPanel.SetActive(true);
    }

    public void ChangeBGM(eScene scene)
    {
        if (BGM_Arr[(int)scene] != null)
            audioManager.ChangeBGM(BGM_Arr[(int)scene]);
    }

    public void Clear()
    {
        bChessClear = false;
        bMovePuzzleClear = false;
        SavePuzzle();
        ClearPanel.SetActive(false);
        LoadScene.LoadingScene("MainScene");
    }

    public void Main()
    {
        OnPause();
        ChangeBGM(eScene.MAIN);
        LoadScene.LoadingScene("MainScene");
    }

    public void Fade_In(float _duration = 0.5f, Action _action = null, Ease _ease = Ease.Linear)
    {
        Image fade_Image = FadePanel.GetComponent<Image>();

        fade_Image.DOFade(0.0f, _duration).OnComplete(() => 
        {
            _action?.Invoke();
            FadePanel.SetActive(false);
        });
    }

    public void Fade_Out(float _duration = 0.5f, Action _action = null, Ease _ease = Ease.Linear)
    {
        Image fade_Image = FadePanel.GetComponent<Image>();

        FadePanel.SetActive(true);
        fade_Image.DOFade(1.0f, _duration).SetEase(_ease).OnComplete(() => 
        {
            _action?.Invoke();
        });
    }

    public void Fade_InOut(float _duration = 0.5f, Action _action = null, Ease _ease = Ease.Linear)
    {
        Image fade_Image = FadePanel.GetComponent<Image>();

        FadePanel.SetActive(true);
        fade_Image.DOFade(1.0f, _duration).OnComplete(() => 
        {
            fade_Image.DOFade(0.0f, 0.5f).OnComplete(() => 
            {
                _action?.Invoke();
                FadePanel.SetActive(false);
            });
        });
    }
}