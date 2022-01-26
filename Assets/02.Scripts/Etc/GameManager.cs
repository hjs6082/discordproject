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
        }

        Time.timeScale = bPause ? 0f : 1f;
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
        PausePanel.SetActive(bPause);
        OptionPanel.SetActive(false);
    }

    private void ClearPanel()
    {
        PausePanel.SetActive(false);
        PausePanel.SetActive(false);
        CreditPanel.SetActive(false);
    }

    public void Pause()
    {
        ClearPanel();
        PausePanel.SetActive(true);
    }

    public void Option()
    {
        ClearPanel();
        OptionPanel.SetActive(true);
    }

    public void Credit()
    {
        ClearPanel();
        CreditPanel.SetActive(true);
    }

    public void ChangeBGM(eScene scene)
    {
        if(BGM_Arr[(int)scene] != null)
        audioManager.ChangeBGM(BGM_Arr[(int)scene]);
    }

    public void Main()
    {
        OnPause();
        ChangeBGM(eScene.MAIN);
        LoadScene.LoadingScene("MainScene");
    }
}