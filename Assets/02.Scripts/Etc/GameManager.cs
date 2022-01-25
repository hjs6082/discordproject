using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    const string fileName = "PuzzleSave";

    public static GameManager Instance { get; private set; }

    public string ManName = "";
    public string WomanName = "";

    public GameObject PauseCanvas;
    public GameObject PausePanel;
    public GameObject OptionPanel;
    public GameObject OptionBGM_Volume;
    public GameObject OptionEFFECT_Volume;
    public bool bPause = false;

    public AudioClip[] BGM_Arr;
    private AudioSource bgmAudio;
    private AudioManager audioManager;

    public bool bChessClear = false;
    public bool bMovePuzzleClear = false;
    public bool bOnIsland = false;

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

    public void ChangeBGM(int index)
    {
        bgmAudio.Stop();
        bgmAudio.clip = BGM_Arr[index];
        bgmAudio.Play();
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

    public void Pause()
    {
        PausePanel.SetActive(true);
        OptionPanel.SetActive(false);
    }

    public void Option()
    {
        PausePanel.SetActive(false);
        OptionPanel.SetActive(true);
    }

    public void Main()
    {
        OnPause();
        LoadScene.LoadingScene("MainScene");
    }
}