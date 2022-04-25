using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjScript : MonoBehaviour
{
/*  public GameObject backGround;
    public Text objText;*/

   // public Object objData;
    private Outline outLine;

    void Start()
    {
        outLine = gameObject.GetComponent<Outline>();
        outLine.enabled = false;
        //outLine.OutlineMode = Outline.Mode.OutlineHidden;
       // backGround.SetActive(false);
    }

    private void OnMouseEnter()
    {
        //objText.text = objData.ObjName;
        outLine.enabled = true;
        outLine.OutlineMode = Outline.Mode.OutlineAll;
        //backGround.SetActive(true);
    }

    private void OnMouseExit()
    {
        //objText.text = "";
        outLine.enabled = false;
        outLine.OutlineMode = Outline.Mode.OutlineHidden;
        //backGround.SetActive(false);
    }

    private void OnMouseDown()
    {
        
    }
}
