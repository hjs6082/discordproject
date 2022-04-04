using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Vector2 moveValue { get; private set; } = Vector2.zero;

    public void MoveInput(InputAction.CallbackContext _inContext)
    {
        moveValue = _inContext.ReadValue<Vector2>();
    }
}
