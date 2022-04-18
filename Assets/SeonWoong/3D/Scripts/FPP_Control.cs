using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Control : MonoBehaviour
{
    private Dictionary<KeyCode, float> fpp_Key_Dic = new Dictionary<KeyCode, float>()
    {
        {KeyCode.A, -45.0f},
        {KeyCode.D, +45.0f}
    };

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if(Input.anyKeyDown)
        {
            foreach(var item in fpp_Key_Dic)
            {
                if(Input.GetKeyDown(item.Key))
                {
                    FPP_Move.rotateAct?.Invoke(item.Value);
                }
            }
        }
    }
}
