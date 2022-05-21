using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishPuzzle : MonoBehaviour
{
    private bool isEnter;
    public bool isCheck;
    public bool isWrong;
    public GameObject greenApple;

    private void OnMouseEnter()
    {
        isEnter = true;
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
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
                    if(EnglishPassword.instance.isClear)
                    {
                        EnglishPassword.instance.DoorOpen();
                        greenApple.SetActive(true);
                    }
                    else
                    {
                        isWrong = true;
                        StartCoroutine(WrongStart());
                    }
                }
            }
        }
    }

    IEnumerator WrongStart()
    {
        yield return new WaitForSeconds(1f);
        isWrong = false;
    }
}
