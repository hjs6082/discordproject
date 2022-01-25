using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeArrow : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.DOFade(0f, 2.0f).SetLoops(-1, LoopType.Yoyo);
    }
}
