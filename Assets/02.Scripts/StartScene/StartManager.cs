using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartManager : MonoBehaviour
{
    public static StartManager Instance { get; private set; }

    private enum eCanvas
    {
        START,
        NICKNAME,
        OPTION,
        CREDIT
    };

    public GameObject[] CanvasArr;

    public InputField ManInput   = null;
    public InputField WomanInput = null;

    public Toggle ShakeToggle = null;
    public bool   bShakeOK    = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        StartCanvas();
    }

    private void Start()
    {
        ShakeToggle.isOn = false;
        bShakeOK = false;
    }

    private void Update()
    {
        if (CanvasArr[(int)eCanvas.OPTION].activeSelf)
        {
            if (bShakeOK != ShakeToggle.isOn)
            {
                bShakeOK = ShakeToggle.isOn;
            }
        }
    } 

    private void ClearCanvas()
    {
        for (int i = 0; i < CanvasArr.Length; i++)
        {
            CanvasArr[i].SetActive(false);
        }
    }

    private void ChangeCanvas(eCanvas _eCanvas)
    {
        int canvas = (int)_eCanvas;
        ClearCanvas();
        CanvasArr[canvas].SetActive(true);
    }

    public void StartCanvas()
    {
        ChangeCanvas(eCanvas.START);
    }

    public void NickNameCanvas()
    {
        ChangeCanvas(eCanvas.NICKNAME);
    }

    public void OptionCanvas()
    {
        ChangeCanvas(eCanvas.OPTION);
    }

    public void CreditCanvas()
    {
        ChangeCanvas(eCanvas.CREDIT);
    }

    public void StartGame()
    {
        GameManager.Instance.isOnLoad = true;
        GameManager.Instance.ClearPanel.SetActive(false);
        GameManager.Instance.Fade_Out(0.5f, () => 
        {
            GameManager.Instance.Fade_In();

            AudioManager.Instance.TeleportSound();
            GameManager.Instance.ManName = (ManInput.text != "") ? ManInput.text : "남편";
            GameManager.Instance.WomanName = (WomanInput.text != "") ? WomanInput.text : "아내";

            GameManager.Instance.ResetVolumeController();
            GameManager.Instance.ChangeBGM(GameManager.eScene.DIALOG);
            LoadScene.LoadingScene("New_Dialog");
        });
    }
}
