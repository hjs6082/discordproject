using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPP_ObjScript : MonoBehaviour
{
    public static FPP_ObjScript instance;
    //public bool isCheck;
/*  public GameObject backGround;
    public Text objText;*/

   // public Object objData;
    private Outline outLine;

    void Start()
    {
        outLine = gameObject.GetComponent<Outline>();
        outLine.enabled = false;
        instance = this;
        //outLine.OutlineMode = Outline.Mode.OutlineHidden;
        //backGround.SetActive(false);
    }

    public void OnOutline(eSight _sight)
    {
        if(_sight == FPP_Manager.Instance.GetMove().curSight)
        {
            outLine.enabled = true;
            outLine.OutlineMode = Outline.Mode.OutlineAll;
        }
    }

    public void OffOutline()
    {
        outLine.enabled = false;
        outLine.OutlineMode = Outline.Mode.OutlineHidden;
    }
}
