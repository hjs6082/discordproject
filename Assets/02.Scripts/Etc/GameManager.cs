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

    public AudioClip[] BGM_Arr;
    private AudioSource bgmAudio;
    private AudioManager audioManager;

    public bool bChessClear = false;
    public bool bMovePuzzleClear = false;
    public bool bOnIsland = false;

    public Vector3 curPlayerPos;

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

        curPlayerPos = new Vector3(-25f, 16f, 10f);
        FadePanel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
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
                CharacterVoice.Instance.audioSource.Pause();
            }
            else
            {
                audioManager.BGM_Source.UnPause();
                audioManager.EFFECT_Source.UnPause();
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

    public void Main()
    {
        OnPause();
        ChangeBGM(eScene.MAIN);
        LoadScene.LoadingScene("MainScene");
    }
}