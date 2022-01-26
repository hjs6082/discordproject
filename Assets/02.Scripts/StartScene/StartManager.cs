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

    public InputField ManInput;
    public InputField WomanInput;

    public Toggle ShakeToggle;
    public bool bShakeOK = true;

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
        GameManager.Instance.FadePanel.SetActive(true);
        GameManager.Instance.FadePanel.GetComponent<Image>().DOFade(1.0f, 0.5f).OnComplete(() =>
        {
            AudioManager.Instance.TeleportSound();
            GameManager.Instance.ManName = (ManInput.text != "") ? ManInput.text : "철수";
            GameManager.Instance.WomanName = (WomanInput.text != "") ? WomanInput.text : "영희";

            GameManager.Instance.ResetVolumeController();
            GameManager.Instance.ChangeBGM(GameManager.eScene.DIALOG);
            LoadScene.LoadingScene("DialogScene");
        });
    }
}
