using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartManager : MonoBehaviour
{
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

    private void Awake()
    {
        StartCanvas();
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
        GameManager.Instance.FadePanel.SetActive(true);
        GameManager.Instance.FadePanel.GetComponent<Image>().DOFade(1.0f, 0.5f).OnComplete(() =>
        {
            GameManager.Instance.ManName = (ManInput.text != "") ? ManInput.text : "철수";
            GameManager.Instance.WomanName = (WomanInput.text != "") ? WomanInput.text : "영희";

            GameManager.Instance.ResetVolumeController();
            GameManager.Instance.ChangeBGM(GameManager.eScene.DIALOG);
            LoadScene.LoadingScene("DialogScene");            
        });
    }
}
