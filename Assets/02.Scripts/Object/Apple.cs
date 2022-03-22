using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Vector3 offset;

    private float zOffset;

    private Vector3 nowOffset;

    private void OnMouseDown()
    {
        zOffset = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        offset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
        nowOffset = transform.position;
        
        this.gameObject.transform.position = new Vector3(nowOffset.x,nowOffset.y + 0.3f, nowOffset.z);
    }
}
