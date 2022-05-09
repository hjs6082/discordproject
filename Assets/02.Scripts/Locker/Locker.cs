using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour
{
    public static Locker instance;

    public bool isCheck;
    public bool isEnter;
    public bool isClear;
    public bool isAnswerCheck;

    private string password = "2134";
    public static string myPassword;
    public string redPassword;
    public string bluePassword;
    public string yellowPassword;
    public string greenPassword;

    private void OnMouseEnter()
    {
        isEnter = true;
    }
    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        myPassword = redPassword + bluePassword     + yellowPassword + greenPassword;

        if (isEnter)
        {
            if(isCheck)
            {
                if (isClear)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        DoorTween.instance.DoorMove();
                    }
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    if(password == myPassword)
                    {
                        isClear = true;

                    }
                    else if(password != myPassword)
                    {
                        StartCoroutine(WrongPassword());
                    }

                }
            }

        }
    }

    IEnumerator WrongPassword()
    {
        isAnswerCheck = true;
        yield return new WaitForSeconds(1f);
        isAnswerCheck = false;
    }

}
