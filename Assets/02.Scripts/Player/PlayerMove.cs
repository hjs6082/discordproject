using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    bool isMove = false;
    public bool isPuzzle = false;

    float rotateEndVal = 0;
    float moveDelay = 0.4f;
    int dur;

    // 레이캐스트 관련
    private RaycastHit hit;
    private float MaxDistance = 3f;

    private Dictionary<KeyCode, float> PlayerMoveAmount = new Dictionary<KeyCode, float>()
    {
        {KeyCode.W, 2.5f},
        {KeyCode.S, -2.5f},
        {KeyCode.A, -90f},
        {KeyCode.D, 90f},
    };

    void Start()
    {
        if (GameManager.Instance != null)
            //this.gameObject.transform.position = GameManager.Instance.curPlayerPos;

            isMove = false;
        rotateEndVal = Quaternion.identity.y;
    }

    void Update()
    {
        if (!isPuzzle)
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
                                bool isForward = (dic.Value > 0) ? true : false;
                                // 레이캐스트 쏴줘서 충돌체 있나 확인
                                if (IsCanMove(isForward))
                                    Straight(dic.Value);
                            }
                            return;
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
        dur = (int)rotateEndVal / 90;

        if (AudioManager.Instance != null)
            AudioManager.Instance.MoveSound();

        switch (dur)
        {
            case 0:
                // z좌표 +
                transform.DOMoveZ(transform.position.z + _moveDistance, moveDelay).OnComplete(() =>
                {
                    isMove = false;
                });
                break;
            case 1:
            case -3:
                // x 좌표 + 
                transform.DOMoveX(transform.position.x + _moveDistance, moveDelay).OnComplete(() =>
                {
                    isMove = false;
                });
                break;
            case -1:
            case 3:
                // x 좌표 -
                transform.DOMoveX(transform.position.x - _moveDistance, moveDelay).OnComplete(() =>
                {
                    isMove = false;
                });
                break;
            case 2:
            case -2:
                // z좌표 -
                transform.DOMoveZ(transform.position.z - _moveDistance, moveDelay).OnComplete(() =>
                {
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
        if (rotateEndVal >= 360 || rotateEndVal <= -360)
        {
            rotateEndVal = 0;
        }
        gameObject.transform.DORotate(new Vector3(0, rotateEndVal, 0), 0.3f, RotateMode.Fast).OnComplete(() =>
        {
            isMove = false;
            print("rotateEnd");
        });
    }

    private bool IsCanMove(bool _isForward) // 레이캐스트
    {
        Vector3 moveDur = (_isForward) ? transform.forward : -transform.forward;

        Debug.DrawRay(transform.position, moveDur, Color.red, 1f);
        if (Physics.Raycast(transform.position, moveDur, out hit, MaxDistance))
        {
            return false;
        }

        return true;
    }
}
