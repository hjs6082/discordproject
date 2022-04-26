using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjScript : MonoBehaviour
{
    public static ObjScript instance;
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
       // backGround.SetActive(false);
    }

    private void OnMouseEnter()
    {
        //objText.text = objData.ObjName;
        /*if (isCheck)
        {*/
            outLine.enabled = true;
            outLine.OutlineMode = Outline.Mode.OutlineAll;
        //}
        //backGround.SetActive(true);
    }

    private void OnMouseExit()
    {
/*        if (!isCheck)
        {*/
            //objText.text = "";
            outLine.enabled = false;
            outLine.OutlineMode = Outline.Mode.OutlineHidden;
            //isCheck = false;
        //}
        //backGround.SetActive(false);
    }

    private void OnMouseDown()
    {
        
    }
}
