using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShakeText : MonoBehaviour, IPointerEnterHandler
{
    float reTime;

    RectTransform rectTrm;

    public bool isIgnore = false;

    public float strength;
    public int vibrato;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isIgnore)
        {
            reTime -= reTime;
            rectTrm.DOShakeRotation(0.1f, strength, vibrato);
        }
        else
        {
            if (StartManager.Instance.bShakeOK)
            {
                reTime -= reTime;
                rectTrm.DOShakeRotation(0.1f, strength, vibrato);
            }
        }
    }

    private void Update()
    {
        if (rectTrm.localRotation != Quaternion.Euler(Vector3.zero))
        {
            reTime += Time.deltaTime;

            if (reTime >= 1f)
            {
                reTime -= reTime;
                rectTrm.DOLocalRotate(Vector3.zero, 0.2f);
            }
        }
    }
}
