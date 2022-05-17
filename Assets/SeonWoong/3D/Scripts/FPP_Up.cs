using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FPP_Up : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Text text = null;

    private void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!FPP_Manager.Instance.GetMove().bObject && FPP_Manager.Instance.GetMove().curSight != eSight.UP)
        {
            FPP_Manager.Instance.bUpDownBtn = true;
            text.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FPP_Manager.Instance.bUpDownBtn = false;
        text.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!FPP_Manager.Instance.GetMove().bObject && FPP_Manager.Instance.GetMove().curSight != eSight.UP)
        {
            int curSight = (int)FPP_Manager.Instance.GetMove().curSight;
            curSight--;

            FPP_Manager.Instance.GetMove().SightUpDown((eSight)curSight);
        }
    }
}
