using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        for(int i = 0; i < CanvasArr.Length; i++)
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
        GameManager.Instance.ManName = ManInput.text;
        GameManager.Instance.WomanName = WomanInput.text;

        LoadScene.LoadingScene("DialogScene");
    }
}
