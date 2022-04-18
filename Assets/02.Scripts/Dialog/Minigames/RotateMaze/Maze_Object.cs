using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Maze_Object : MonoBehaviour
{
    public static Action check;
    [SerializeField] private Transform endPoint = null;

    private void Awake() {
        check += CheckBottom;
    }

    public void CheckBottom()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down * 5);
        Debug.DrawRay(this.transform.position, Vector3.down * 5, Color.red, 1f);
        if(hit.transform.tag.Equals("BOARD"))
        {
            this.transform.DOMoveY(hit.transform.position.y + (this.transform.localScale.y / 2) + 0.25f, 0.5f)
            .SetEase(Ease.OutCirc)
            .OnComplete(() => 
            {
                CheckClear(); 
                Maze_Manager.Instance.canRotate = true;
            });
        }
    }

    public void CheckClear()
    {
        if(this.transform.position == endPoint.position)
        {
            Debug.Log("Clear!!");
            // TODO : 클리어 처리              
        }
    }
}
