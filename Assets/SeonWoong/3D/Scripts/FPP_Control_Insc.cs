using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Control_Insc : MonoBehaviour
{
    private Dictionary<KeyCode, float> fpp_Key_Dic = new Dictionary<KeyCode, float>()
    {
        // rotate
        {KeyCode.A, -45.0f},
        {KeyCode.D, +45.0f},

        // move
        {KeyCode.W, + 1.0f},
        {KeyCode.S, - 1.0f}
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
                    if(item.Key == KeyCode.A || item.Key == KeyCode.D)
                    {
                        FPP_Move_Insc.rotateAct?.Invoke(item.Value);
                    }
                    else
                    {
                        // TODO : 전진 후진
                        FPP_Move_Insc.moveAct?.Invoke(item.Value);
                    }
                    return;
                }
            }

            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                FPP_Manager.Instance.GetMove().SitUpDown();
            }
        }
    }
}
