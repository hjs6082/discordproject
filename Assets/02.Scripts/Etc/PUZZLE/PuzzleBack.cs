using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PuzzleBack : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image backButtonImage;

    private Color SetColor(float r, float g, float b, float a)
    {
        return new Color(r / 255f, g / 255f, b / 255f, a / 255f);
    }

    private void Awake()
    {
        backButtonImage = this.GetComponent<Image>();

        backButtonImage.color = SetColor(255f, 255f, 255f, 0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backButtonImage.color = SetColor(255f, 255f, 255f, 100f);
        this.GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backButtonImage.color = SetColor(255f, 255f, 255f, 0f);
        this.GetComponentInChildren<Text>().color = Color.clear;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BackPuzzle();
    }

    private void BackPuzzle()
    {
        LoadScene.LoadingScene("MoveScene");
    }
}
