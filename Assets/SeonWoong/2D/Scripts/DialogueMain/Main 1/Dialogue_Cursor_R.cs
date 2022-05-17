using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialogue_Cursor_R : MonoBehaviour
{
    private static readonly Vector3 cursor_EndVal = new Vector3(490.0f, -100.0f, 0.0f);

    public Sprite cursor_Image = null;
    private Image cursor = null;

    private void Awake()
    {
        cursor = GetComponent<Image>();
    }

    private void Start()
    {
        cursor.sprite = cursor_Image;
    }

    public void MoveCursor(Action _callback)
    {
        cursor.rectTransform.DOAnchorPos3D(cursor_EndVal, 0.75f)
        .SetEase(Ease.InOutCubic)
        .OnComplete(() => 
        {
            StartCoroutine(Delay(0.25f, _callback));
        });
    }

    IEnumerator Delay(float _duration, Action _callback)
    {
        yield return new WaitForSeconds(_duration);

        _callback?.Invoke();
    }
}
