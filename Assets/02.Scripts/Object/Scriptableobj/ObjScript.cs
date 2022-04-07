using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjScript : MonoBehaviour
{
    public Text objText;

    public Object objData;

    private void OnMouseEnter()
    {
        Debug.Log("#24");
        objText.text = objData.ObjName;
    }

    private void OnMouseExit()
    {
        Debug.Log("234");
        objText.text = "";
    }

    private void OnMouseDown()
    {
        Debug.Log("654");
    }
}
