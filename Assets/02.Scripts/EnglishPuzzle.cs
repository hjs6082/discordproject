using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnglishPuzzle : MonoBehaviour
{
    private bool isEnter;
    public bool isCheck;
    public bool isWrong;
    public GameObject greenApple;
    public bool isPuzzleOn;
    public bool isPuzzleClear;
    private bool isOn;
    public GameObject plr;

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
        if(EnglishPassword.instance.isClear)
        {
            if (!isOn)
            {
                isPuzzleClear = true;
                if (CameraSwitcher.IsActiveCamera(Suntail.PlayerInteractions.instance.passwordPuzzleCam))
                {
                    CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.firstPersonCam);
                }

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Suntail.PlayerInteractions.instance.point.SetActive(true);
                plr.GetComponent<Suntail.PlayerController>().enabled = true;
                isPuzzleOn = false;
                greenApple.SetActive(true);
                isOn = false;
            }
        }
        if(isEnter)
        {
            if(isCheck)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    if (!isPuzzleOn)
                    {
                        if (CameraSwitcher.IsActiveCamera(Suntail.PlayerInteractions.instance.firstPersonCam))
                        {
                            CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.passwordPuzzleCam);
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                            Suntail.PlayerInteractions.instance.point.SetActive(false);
                            plr.GetComponent<Suntail.PlayerController>().enabled = false;
                        }
                        isPuzzleOn = true;
                    }

                    if (EnglishPassword.instance.isClear)
                    {
                        EnglishPassword.instance.DoorOpen();

                    }
                    else
                    {
                        if (isPuzzleOn)
                        {
                            isWrong = true;
                            StartCoroutine(WrongStart());
                        }
                    }
                }
            }
        }
        if(isPuzzleOn)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (CameraSwitcher.IsActiveCamera(Suntail.PlayerInteractions.instance.passwordPuzzleCam))
                {
                    CameraSwitcher.SwitchCamera(Suntail.PlayerInteractions.instance.firstPersonCam);
                }
                
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Suntail.PlayerInteractions.instance.point.SetActive(true);
                plr.GetComponent<Suntail.PlayerController>().enabled = true;
                isPuzzleOn = false;
            }
        }
    }

    IEnumerator WrongStart()
    {
        yield return new WaitForSeconds(1f);
        isWrong = false;
    }
}
