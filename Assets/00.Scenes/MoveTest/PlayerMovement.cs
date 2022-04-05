using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private const float WALK_SPEED = 10.0f;
    private const float RUN_SPEED = 15.0f;

    private PlayerInput pInput = null;

    public Transform player;
    public Vector3 offset;

    public Vector2 moveValue = Vector2.zero;
    private float curMoveSpeed = 0.0f;
    private bool isJump = false;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        curMoveSpeed = WALK_SPEED;
    }

    private void Update()
    {
        CameraPos();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void CameraPos()
    {
        this.transform.position = player.position + offset;
    }

    private void ControlSpeed()
    {
        curMoveSpeed = (pInput.bRun) ? RUN_SPEED : WALK_SPEED;
    }

    private void MovePlayer()
    {
        Vector2 _moveValue = pInput.moveValue;
        Vector3 _moveDir = new Vector3(_moveValue.x, 0.0f, _moveValue.y);
        _moveDir.Normalize();

        ControlSpeed();
        player.position += _moveDir * Time.fixedDeltaTime * curMoveSpeed;
    }
}
