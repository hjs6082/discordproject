using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestTween : MonoBehaviour
{
    public static ChestTween instance;
    public GameObject chestObj;
    public GameObject[] apples;

     void Awake()
    {
        instance = this;   
    }

    public void ChestMove()
    {
        chestObj.transform.DOLocalRotate(new Vector3(-90, 0, 0), 2f);
        for(int i = 0; i < apples.Length; i++)
        {
            apples[i].SetActive(true);
        }
    }
}
