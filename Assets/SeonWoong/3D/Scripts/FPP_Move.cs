using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum eSight
{
    UP,
    MIDDLE,
    DOWN
}

public class FPP_Move : MonoBehaviour
{
    public static Action<float> rotateAct = null;
    public static Action<float> moveAct = null;

    public Transform player { get; private set; }

    private Camera mainCam = null;
    private RaycastHit hit;

    public eSight curSight = eSight.MIDDLE;
    private Vector3 curPlayerSight = new Vector3(0, 0, 0);
    private float curSightOffset = 0.0f;

    public bool bObject = false;
    public bool bSightDown = false;
    public bool bSitDown = false;
    private bool isRotate = false;
    private bool isMove = false;

    public float moveDisOffset = 0.0f;

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
        moveAct += (x) => MovePlayer(x);

        mainCam = Camera.main;
        player = this.transform;
    }

    private void FollowRotate()
    {
        mainCam.transform.rotation = player.rotation;
    }

    public bool IsCanMove(float _dir)
    {
        Vector3 rayOrigin = new Vector3(player.position.x, 3.3f, player.position.z);

        Debug.Log(player.eulerAngles.y);
        float maxDistance = (Mathf.Abs(Mathf.Round(player.eulerAngles.y)) % 10.0f != 5.0f) ? 1.0f : 1.5f;

        bool isRay = Physics.Raycast(rayOrigin, player.forward * _dir, out hit, maxDistance);

        Debug.DrawRay(player.position, player.forward, Color.red, 1f);
        Debug.Log(maxDistance);

        return isRay;
    }

    public void RotatePlayer(float _degree)
    {
        if (!isRotate && !isMove && !bObject && !bSightDown)
        {
            isRotate = true;

            Vector3 curPlayerRotate = player.rotation.eulerAngles;
            Vector3 degreeOffset = new Vector3(0.0f, _degree, 0.0f);

            Vector3 playerEndRotate = curPlayerRotate + degreeOffset;

            player.DORotate(playerEndRotate, 0.125f)
            .OnComplete(() =>
            {
                isRotate = false;
            });
        }
    }

    public void MovePlayer(float _dir)
    {
        if (!isMove && !isRotate && !bObject && !IsCanMove(_dir) && !bSightDown)
        {
            isMove = true;

            Debug.Log(player.forward);
            Vector3 forward = new Vector3(Mathf.Round(player.forward.x), 0.0f, Mathf.Round(player.forward.z));
            Vector3 moveEndValue = player.position + forward * moveDisOffset * _dir;

            player.DOMove(moveEndValue, 0.15f)
            .OnComplete(() =>
            {
                isMove = false;
            });
        }
    }

    public void SightUpDown(eSight _sight)
    {
        if (!isRotate && !isMove && !bObject)
        {
            isRotate = true;

            curPlayerSight = Camera.main.transform.localEulerAngles;
            curSight = _sight;

            switch (curSight)
            {
                case eSight.UP:
                    {
                        curSightOffset = -30.0f;
                        bSightDown = true;
                    }
                    break;
                case eSight.DOWN:
                    {
                        curSightOffset = 60.0f;
                        bSightDown = true;
                    }
                    break;
                default:
                    {
                        curSightOffset = 0.0f;
                        bSightDown = false;
                    }
                    break;
            }

            if (curSightOffset != curPlayerSight.x)
            {
                Vector3 sightOffset = new Vector3(curSightOffset, curPlayerSight.y, 0.0f);

                Camera.main.transform.DOLocalRotate(sightOffset, 0.15f)
                .OnComplete(() =>
                {
                    isRotate = false;
                });
            }
        }
    }

    public void SitUpDown()
    {
        if (!isRotate && !isMove && !bObject)
        {
            isMove = true;
            bSitDown = !bSitDown;

            float endVal = (!bSitDown) ? 2.5f : 3.3f;

            gameObject.transform.DOMoveY(endVal, 0.25f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                isMove = false;
            });
        }
    }
}
