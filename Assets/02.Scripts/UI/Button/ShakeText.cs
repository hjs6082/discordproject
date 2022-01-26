using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ShakeText : MonoBehaviour, IPointerEnterHandler
{
    float reTime;

    RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (StartManager.Instance.bShakeOK)
        {
            reTime -= reTime;
            rectTrm.DOShakeRotation(0.1f);
        }
    }

    private void Update()
    {
        if (reTime != 0.0f)
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
