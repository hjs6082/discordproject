using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FPP_Move : MonoBehaviour
{
    private const float MOVE_SPEED = 3.0f;
    private const float LOOK_SPEED = 2.0f;
    private const float LOOK_X_LIMIT = 70.0f;

    public static Action rotateAct = null;
    public static Action moveAct = null;

    public  Transform player     { get; private set; }
    public BoxCollider playerCol = null;
    private Camera mainCam = null;

    private float cameraPosZ = 0.0f;
    private float moveX   = 0.0f;
    private float moveZ   = 0.0f;
    private float curRotateX = 0.0f;
    private float curRotateY = 0.0f;

    private bool canSit = true;

    public bool bObject  = false;
    public bool bSit = false;
    private bool isRotate = false;

    public float moveDisOffset = 0.0f;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        // if (isRotate)
        // {
        //     FollowRotate();
        // }

        if(!bObject)
        {
            FPP_Control.inputAct?.Invoke();

            rotateAct?.Invoke();
        }
    }

    private void InitValue()
    {
        rotateAct += () => RotatePlayer();
        moveAct += () => MovePlayer();

        mainCam = Camera.main;
        player = this.transform;

        Debug.Log("rotation" + player.rotation);
        Debug.Log("localRotation" + player.localRotation);

        curRotateX = -180.0f;
        curRotateY = mainCam.transform.localRotation.x;
    }

    private void FollowRotate()
    {
        mainCam.transform.localRotation = player.localRotation;
    }

    public void RotatePlayer()
    {
        float rotateX = Input.GetAxis("Mouse X") * LOOK_SPEED;
        float rotateY = Input.GetAxis("Mouse Y") * -LOOK_SPEED;

        if(rotateX != 0.0f || rotateY != 0.0f)
        {
            //isRotate = true;
            curRotateX += rotateX;
            curRotateY += rotateY;

            curRotateY = Mathf.Clamp(curRotateY, -LOOK_X_LIMIT, LOOK_X_LIMIT);

            Debug.Log(curRotateX);

            mainCam.transform.localRotation = Quaternion.Euler(curRotateY, 0.0f, 0.0f);
            player.localRotation = Quaternion.Euler(0.0f, curRotateX, 0.0f);
            
            return;
        }
        

        // if(isRotate)
        // {
        //     isRotate = false;
        // }
    }

    public void MovePlayer()
    {
        moveX = Input.GetAxis("Vertical") * MOVE_SPEED;
        moveZ = Input.GetAxis("Horizontal") * MOVE_SPEED;

        if(moveX != 0.0f || moveZ != 0.0f)
        {
            Vector3 moveOffset = ((player.forward * moveX) + (player.right * moveZ)) * Time.deltaTime;
            moveOffset.y = 0.0f;

            player.position += moveOffset;

            return;
        }
    }

    public void SitUpDown()
    {
        if(canSit)
        {
            canSit = false;
            bSit = !bSit;

            float endVal = (!bSit) ? 2.5f : 3.3f;

            gameObject.transform.DOMoveY(endVal, 0.25f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                canSit = true;
            });
        }
    }
}
