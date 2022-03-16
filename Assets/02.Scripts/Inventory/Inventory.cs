using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Image selectImage1;
    [SerializeField]
    public Image selectImage2;

    public bool isSelect = false;
    void Start()
    {
        ClearSelect();
    }

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            if (isSelect == false)
            {
                ClearSelect();
                selectImage1.enabled = true;
                isSelect = true;
            }
            else if(selectImage1.enabled == false || selectImage2.enabled == true)
            {
                ClearSelect();
                isSelect = true;
                selectImage1.enabled = true;
            }
            else
            {
                selectImage1.enabled = false;
                isSelect = false;
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (isSelect == false)
            {
                ClearSelect();
                selectImage2.enabled = true;
                isSelect = true;
            }
            else if (selectImage1.enabled == true || selectImage2.enabled == false)
            {
                ClearSelect();
                isSelect = true;
                selectImage2.enabled = true;
            }
            else
            {
                selectImage2.enabled = false;
                isSelect = false;
            }


        }
    }

    void ClearSelect()
    {
        selectImage1.enabled = false;
        selectImage2.enabled = false;
    }
}
