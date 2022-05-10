using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ChestTween : MonoBehaviour
{
    public static ChestTween instance;
    public GameObject[] apples;
    public bool isOpen;

     void Awake()
    {
        instance = this;   
    }

    public void ChestMove()
    {
        if (!isOpen)
        {
            this.gameObject.transform.DOLocalRotate(new Vector3(-90, 0, 0), 2f);
            if (apples[0] != null)
            {
                for (int i = 0; i < apples.Length; i++)
                {
                    apples[i].SetActive(true);
                }
            }
            isOpen = true;
        }
    }

    public void ChestClose()
    {
        isOpen = false;
        this.gameObject.transform.DOLocalRotate(new Vector3(0, 0, 0), 2f);
    }
}
