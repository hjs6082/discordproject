using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBang_Control : MonoBehaviour
{
    public BingBang_Obj bingBang_Obj { get; private set; }
    private Quaternion obj_Rotation = Quaternion.identity;
    private Quaternion twinkle_Rotation = Quaternion.identity;

    private void Awake()
    {
        bingBang_Obj = GetComponentInChildren<BingBang_Obj>();
    }

    public bool TwinkleTurn()
    {
        if(Input.anyKeyDown)
        {
            bingBang_Obj.ReverseSpeed();
        }

        if(Mathf.Abs(obj_Rotation.z - twinkle_Rotation.z) <= 5.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
