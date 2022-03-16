using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyFairy : MonoBehaviour
{
    private RectTransform rectTrm;
    private Sequence seq;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
    }

    private void Start() 
    {
        seq = DOTween.Sequence();
        seq.Append(rectTrm.DOAnchorPosY(260f, 1.0f).SetEase(Ease.Linear));
        seq.Append(rectTrm.DOAnchorPosY(200f, 1.0f).SetEase(Ease.Linear));
        seq.AppendCallback(() => seq.Restart());
    }
}
