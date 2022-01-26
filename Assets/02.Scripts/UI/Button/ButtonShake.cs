using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonShake : MonoBehaviour, IPointerEnterHandler
{
    float reTime;

    RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        reTime -= reTime;
        rectTrm.DOShakeRotation(0.1f);
    }

    private void Update()
    {
        reTime += Time.deltaTime;

        if(reTime >= 1f)
        {
            rectTrm.DOLocalRotate(Vector3.zero, 0.2f);
        }
    }
}
