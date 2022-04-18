using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FullPicture : MonoBehaviour
{
    public static event Action clickAction;

    private void OnEnable()
    {
        Debug.Log("@34234");
        clickAction?.Invoke();
    }
    
}
