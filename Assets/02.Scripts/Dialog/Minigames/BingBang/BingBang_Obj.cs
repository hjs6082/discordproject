using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBang_Obj : MonoBehaviour
{
    private const float DEFAULT_ROTATE_SPEED = 200.0f;
    private float rotateSpeed = 0.0f;

    public GameObject rotateObj = null;
    public GameObject pointObj = null;
    public GameObject twinkle = null;

    private void Start()
    {
        rotateSpeed = DEFAULT_ROTATE_SPEED;
    }

    public void TwinkleRotate()
    {
        rotateObj.transform.RotateAround(pointObj.transform.position, Vector3.forward, Time.deltaTime * rotateSpeed);
    }
}
