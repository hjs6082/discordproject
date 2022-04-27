using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Manager : MonoBehaviour
{
    private FPP_Move    fpp_Move = null;
    private FPP_Control fpp_Ctrl = null;

    private void Awake()
    {
        
    }

    private void InitClass()
    {
        fpp_Move = GetComponent<FPP_Move>();
        fpp_Ctrl = GetComponent<FPP_Control>();
    }
}
