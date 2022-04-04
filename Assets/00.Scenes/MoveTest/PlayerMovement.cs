using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private const float MOVE_SPEED = 10.0f;

    private PlayerInput pInput = null;

    public Transform player;
    public Vector3 offset;

    public Vector2 moveValue = Vector2.zero;
    private bool isJump = false;

    private void Awake()
    {
        pInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        CameraPos();
        MovePlayer();
    }

    private void CameraPos()
    {
        this.transform.position = player.position + offset;
    }

    private void MovePlayer()
    {
        Vector2 _moveValue = pInput.moveValue;
        Vector3 _moveDir = new Vector3(_moveValue.x, 0.0f, _moveValue.y);
        _moveDir.Normalize();
        player.position += _moveDir * Time.deltaTime * MOVE_SPEED;
    }
}
