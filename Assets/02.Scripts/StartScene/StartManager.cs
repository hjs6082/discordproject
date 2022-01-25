using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
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

    public void StartCanvas()
    {
        ClearCanvas();
        CanvasArr[0].SetActive(true);
    }

    public void NickNameCanvas()
    {
        ClearCanvas();
        CanvasArr[1].SetActive(true);
    }

    public void StartGame()
    {
        GameManager.Instance.ManName = ManInput.text;
        GameManager.Instance.WomanName = WomanInput.text;

        LoadScene.LoadingScene("DialogScene");
    }
}
