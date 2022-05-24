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

    public bool isPuzzleOn;

    public GameObject plr;

    private bool isOn;

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

        if (password == myPassword)
        {
            isClear = true;

        }
        if (isEnter)
        {
            if(isCheck)
            {
                if (isClear)
                {
                    if (!isOn)
                    {
                        CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.firstPersonCam);
                        Cursor.lockState = CursorLockMode.Locked;
                        Cursor.visible = false;
                        plr.GetComponent<Suntail.PlayerController>().enabled = true;
                        Suntail.PlayerInteractions.instance.point.SetActive(true);
                        isPuzzleOn = false;
                        isOn = true;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        DoorTween.instance.DoorMove();
                    }
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    if (!isPuzzleOn && !isClear)
                    {
                        CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.lockerPuzzleCam);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        Suntail.PlayerInteractions.instance.point.SetActive(false);
                        plr.GetComponent<Suntail.PlayerController>().enabled = false;
                        isPuzzleOn = true;
                    }
                    else if(password != myPassword)
                    {
                        StartCoroutine(WrongPassword());
                    }

                }
            }

        }

        if(isPuzzleOn)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.firstPersonCam);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Suntail.PlayerInteractions.instance.point.SetActive(true);
                plr.GetComponent<Suntail.PlayerController>().enabled = true;
                isPuzzleOn = false;
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
