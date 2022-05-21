using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPP_Outline : MonoBehaviour
{  
    private Outline outline = null;

    private void Awake()
    {
        outline = GetComponent<Outline>();

        outline.enabled = false;
    }

    public void OnOutline()
    {
        outline.enabled = true;
        outline.OutlineMode = Outline.Mode.OutlineAll;
    }

    public void OffOutline()
    {
        outline.OutlineMode = Outline.Mode.OutlineHidden;
        outline.enabled = false;
    }
}