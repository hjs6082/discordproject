using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FPP_Move : MonoBehaviour
{
    public static Action<float> rotateAct = null;

    private Camera mainCam = null;
    private Transform player = null;
    private bool isRotate = false;

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

        mainCam = Camera.main;
        player = this.transform;
    }

    private void FollowRotate()
    {
        mainCam.transform.rotation = player.rotation;
    }

    public void RotatePlayer(float _degree)
    {
        if (!isRotate)
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
}
