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

    #region 싱글톤
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameManager instance = null;
    #endregion

    public string ManName   = string.Empty;
    public string WomanName = string.Empty;

    public GameObject FadePanel = null;
    public bool isOnLoad;

    public  Book_Main Book    = null;
    private ePage     curPage = ePage.LIST;
    private Dictionary<KeyCode, ePage> page_Key_Dic = new Dictionary<KeyCode, ePage>()
    {
        {KeyCode.L,      ePage.LIST  },
        {KeyCode.P,      ePage.ALBUM },
        {KeyCode.M,      ePage.MAP   },
        {KeyCode.Escape, ePage.OPTION}
    };


    public Vector3    curPlayerRotate     = new Vector3(0.0f, 0.0f, 0.0f);
    public GameObject PlayerObject        = null;
    public GameObject PauseCanvas         = null;
    public GameObject PausePanel          = null;
    public GameObject OptionPanel         = null;
    public GameObject CreditPanel         = null;
    public GameObject OptionBGM_Volume    = null;
    public GameObject OptionEFFECT_Volume = null;

    public bool bPause   = false;
    public bool isPuzzle = false;

    public  AudioClip[]  BGM_Arr;
    private AudioSource  bgmAudio;
    private AudioManager audioManager;

    public bool bChessClear      = false;
    public bool bMovePuzzleClear = false;
    public bool bOnIsland        = false;

    public bool stage1Start = false;

    public GameObject ClearPanel;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

        //curPlayerPos = new Vector3(-25f, 16f, 10f);
        audioManager = GetComponentInChildren<AudioManager>();
        audioManager?.InitVolumeSettings();
        
        InitObject();        

        SaveData savePuzzle = SaveSystem.Load(fileName);

        if (savePuzzle != null)
        {
            bChessClear = savePuzzle.bChessClear;
            bMovePuzzleClear = savePuzzle.bMovePuzzleClear;
            bOnIsland = savePuzzle.bOnIsland;
        }

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
            InputPause();
        }
        else if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void InitObject()
    {
        PauseCanvas.SetActive(false);
        ClearPanel.SetActive(false);

        FadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        FadePanel.SetActive(false);
    }

    private void InputPause()
    {
        if(Book == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
                return;
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                foreach (var page in page_Key_Dic)
                {
                    if (Input.GetKeyDown(page.Key))
                    {
                        if(bPause)
                        {
                            if(page.Value == curPage)
                            {
                                BookCtrl(page.Value);
                            }
                            else
                            {
                                curPage = page.Value;
                                Book.FlipButton((int)curPage);
                            }
                        }
                        else
                        {
                            curPage = page.Value;
                            BookCtrl(page.Value);
                        }

                        return;
                    }
                }
            }
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

    private void InitPanel()
    {
        PausePanel.SetActive(false);
        OptionPanel.SetActive(false);
        CreditPanel.SetActive(false);
    }

    public void BookCtrl(ePage _page)
    {
        bPause = !bPause;

        Book.FlipButton((int)_page);
        
        Book.OnOffBook(bPause);
    }

    public void Pause()
    {
        bPause = !bPause;

        if (PlayerObject != null) {  curPlayerRotate = PlayerObject.transform.rotation.eulerAngles; }

        if (bPause)
        {
            Cursor.lockState = CursorLockMode.None;

            audioManager?.BGM_Source.Pause();
            audioManager?.EFFECT_Source.Pause();

            if (CharacterVoice.Instance != null) { CharacterVoice.Instance.audioSource.Pause(); }

            if (PlayerObject != null) { PlayerObject.transform.rotation = Quaternion.Euler(curPlayerRotate); }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            audioManager?.BGM_Source.UnPause();
            audioManager?.EFFECT_Source.UnPause();

            if (CharacterVoice.Instance != null) { CharacterVoice.Instance.audioSource.UnPause(); }
        }

        PauseCanvas.SetActive(bPause);
        InitPanel();
        PausePanel.SetActive(bPause);
    }

    private void ChangeTimeScale()
    {
        Time.timeScale = bPause ? 0.0f : 1.0f;
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
            audioManager?.ChangeBGM(BGM_Arr[(int)scene]);
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
        Pause();
        ChangeBGM(eScene.MAIN);
        LoadScene.LoadingScene("MainScene");
    }

    public void Fade_In(float _duration = 0.5f, Action _action = null, Ease _ease = Ease.Linear)
    {
        Image fade_Image = FadePanel.GetComponent<Image>();

        fade_Image.DOFade(0.0f, _duration)
        .SetDelay(0.5f)
        .OnComplete(() => 
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

    public void Fade_OutIn(float _duration = 0.5f, Action _action = null, Ease _ease = Ease.Linear)
    {
        Image fade_Image = FadePanel.GetComponent<Image>();

        FadePanel.SetActive(true);
        fade_Image.DOFade(1.0f, _duration).OnComplete(() => 
        {
            _action?.Invoke();
            fade_Image.DOFade(0.0f, 0.5f).OnComplete(() => 
            {
                FadePanel.SetActive(false);
            });
        });
    }
}