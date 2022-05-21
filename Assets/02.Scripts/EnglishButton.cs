using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnglishButton : MonoBehaviour
{
    public bool isEnter;
    public bool isCheck;
    public TextMeshProUGUI myPassword;

    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter)
        {
            if(isCheck)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if (!EnglishPassword.instance.isCheck)
                    {
                        EnglishPassword.instance.englishPassword.text += myPassword.text;
                    }
                }
            }
        }
    }
}
