using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBang_Obj : MonoBehaviour
{
    private const float DEFAULT_ROTATE_SPEED = 200.0f;
    private float rotateSpeed = 0.0f;

    public GameObject rotateObj = null; // 돌아가는 오브젝트
    public GameObject pointObj = null; // 돌아갈 중심 오브젝트
    public GameObject twinkle = null; // 

    private void Start()
    {
        rotateSpeed = DEFAULT_ROTATE_SPEED;
    }

    public void TwinkleRotate()
    {
        rotateObj.transform.RotateAround(pointObj.transform.position, Vector3.forward, Time.deltaTime * rotateSpeed);
    }

    public void ReverseSpeed()
    {
        RandomTwinkle();

        rotateSpeed *= -1.0f;
    }

    public void RandomTwinkle()
    {
        float randAngle = Random.Range(75.0f, 285.0f); 
        twinkle.transform.RotateAround(pointObj.transform.position, Vector3.forward, randAngle);
    }
}
