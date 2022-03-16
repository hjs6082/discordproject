using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class DrawerOpen : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 goalPosition;
    private void Awake()
    {
        startPosition = this.gameObject.transform.position;
        goalPosition = new Vector3(startPosition.x + 1.4f, startPosition.y, startPosition.z);
    }
    private void OnMouseDown()
    {
        if(this.gameObject.transform.position.x == startPosition.x)
        {
            this.gameObject.transform.DOMove(new Vector3(goalPosition.x,goalPosition.y,goalPosition.z), 1f, false).SetEase(Ease.InQuad);
        }
        else if(this.gameObject.transform.position.x == goalPosition.x)
        {
            this.gameObject.transform.DOMove(new Vector3(startPosition.x, startPosition.y, startPosition.z), 1f, false).SetEase(Ease.InQuad);
        }
        else
        {

        }
    }
}
