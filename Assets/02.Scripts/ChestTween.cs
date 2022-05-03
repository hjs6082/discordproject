using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestTween : MonoBehaviour
{
    public static ChestTween instance;
    public GameObject chestObj;

     void Awake()
    {
        instance = this;   
    }

    public void ChestMove()
    {
        chestObj.transform.DOLocalRotate(new Vector3(-90, 0, 0), 2f);
    }
}
