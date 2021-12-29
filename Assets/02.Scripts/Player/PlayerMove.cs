using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    bool isMove = false;

    float rotateEndVal = 0;
    float moveDelay = 0.25f;
    int dur = 0;

    // 레이캐스트 관련
    private RaycastHit hit;

    private Dictionary<KeyCode, float> PlayerMoveAmount = new Dictionary<KeyCode, float>()
    {
        {KeyCode.W, 2.5f},
        {KeyCode.S, -2.5f},
        {KeyCode.A, -90f},
        {KeyCode.D, 90f},
    };

    void Start()
    {
        isMove = false;
        rotateEndVal = Quaternion.identity.y;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in PlayerMoveAmount)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    if (!isMove)
                    {
                        if (Mathf.Abs(dic.Value) > 5)
                        {
                            Rotate(dic.Value);
                        }
                        else
                        {
                            // 레이캐스트 쏴줘서 충돌체 있나 확인
                            dur = (int)rotateEndVal / 90;
                            if(IsCanMove())
                            Straight(dic.Value);
                        }
                    }
                }
            }
        }
    }

    private void Straight(float _moveDistance)
    {
        print("move");
        isMove = true;

        switch(dur)
        {
            case 0:
            // z좌표 +
            transform.DOMoveZ(transform.position.z + _moveDistance, moveDelay).OnComplete(() => {
               isMove = false; 
            });
            break;
            case 1:
            case -3:
            // x 좌표 + 
            transform.DOMoveX(transform.position.x + _moveDistance, moveDelay).OnComplete(() => {
               isMove = false; 
            });
            break;
            case -1:
            case 3:
            // x 좌표 -
            transform.DOMoveX(transform.position.x - _moveDistance, moveDelay).OnComplete(() => {
               isMove = false; 
            });
            break;
            case 2:
            case -2:
            // z좌표 -
            transform.DOMoveZ(transform.position.z - _moveDistance, moveDelay).OnComplete(() => {
               isMove = false; 
            });
            break;
            default:
            break;
        }
    }

    private void Rotate(float _rotateAmount)
    {
        print("rotate");
        isMove = true;
        rotateEndVal += _rotateAmount;
        if(rotateEndVal >= 360 || rotateEndVal <= -360)
        {
            rotateEndVal = 0;
        }
        gameObject.transform.DORotate(new Vector3(0, rotateEndVal, 0), 0.3f, RotateMode.Fast).OnComplete(() =>
        {
            isMove = false;
            print("rotateEnd");
        });
    }

    private bool IsCanMove() // 레이캐스트
    {
        switch(dur)
        {
            case 0:
            break;

            case 1:
            case -3:
            break;

            case -1:
            case 3:
            break;

            case 2:
            case -2:
            break;

            default:
            break;
        }

        if(Physics.Raycast(transform.position, ))
        {

        }

        return false;
    }
}
