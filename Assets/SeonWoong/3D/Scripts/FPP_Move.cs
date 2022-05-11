using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FPP_Move : MonoBehaviour
{
    public static Action<float> rotateAct = null;
    public static Action<float> moveAct   = null;

    private Camera mainCam   = null;
    private Transform player = null;

    private bool isRotate = false;
    private bool isMove   = false;

    private void Awake()
    {
        InitValue();
    }

    private void Update()
    {
        if (isRotate)
        {
            FollowRotate();
        }
    }

    private void InitValue()
    {
        rotateAct += (x) => RotatePlayer(x);
        moveAct   += (x) => MovePlayer(x); 

        mainCam = Camera.main;
        player = this.transform;
    }

    private void FollowRotate()
    {
        mainCam.transform.rotation = player.rotation;
    }

    public void RotatePlayer(float _degree)
    {
        if (!isRotate && !isMove)
        {
            isRotate = true;

            Vector3 curPlayerRotate = player.rotation.eulerAngles;
            Vector3 degreeOffset = new Vector3(0.0f, _degree, 0.0f);
            
            Vector3 playerEndRotate = curPlayerRotate + degreeOffset;

            player.DORotate(playerEndRotate, 0.25f)
            .OnComplete(() =>
            {
                isRotate = false;
            });
        }
    }

    public void MovePlayer(float _dir)
    {
        if(!isMove && !isRotate)
        {
            isMove = true;

            Debug.Log(player.forward);
            Vector3 forward = new Vector3(Mathf.Round(player.forward.x), 0.0f, Mathf.Round(player.forward.z));
            Vector3 moveEndValue = player.position + forward * _dir;

            player.DOMove(moveEndValue, 0.25f)
            .OnComplete(() => 
            {
                isMove = false;
            });
        }
    }
}
