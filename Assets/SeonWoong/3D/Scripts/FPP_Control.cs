using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Control : MonoBehaviour
{
    public static Action inputAct = null;

    private void Awake()
    {
        inputAct += InputKey;
    }

    private void InputKey()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            FPP_Move.moveAct?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FPP_Manager.Instance.GetMove().SitUpDown();
        }
    }
}