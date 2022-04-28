using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockerPassword : MonoBehaviour
{
    public enum type
    {
        RedButton,
        BlueButton,
        GreenButton,
        YellowButton
    };

    public type ButtonType;
    public TextMeshProUGUI lockerText;
    private bool isEnter;
    private int number = 0;
    public bool isCheck;

    private void Start()
    {
        lockerText.text = number.ToString();
    }
    private void OnMouseEnter()
    {
        isEnter = true;
        
    }

    private void OnMouseExit()
    {
        isEnter = false;
        isCheck = false;
    }

    private void Update()
    {
        if(isEnter)
        {
            if(isCheck)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    number++;
                    if(number >= 10)
                    {
                        number = 0;
                    }
                    lockerText.text = number.ToString();
                    switch (ButtonType)
                    {
                        case type.RedButton:
                            Locker.instance.redPassword = number.ToString();
                            break;
                        case type.BlueButton:
                            Locker.instance.bluePassword = number.ToString();
                            break;
                        case type.GreenButton:
                            Locker.instance.greenPassword = number.ToString();
                            break;
                        case type.YellowButton:
                            Locker.instance.yellowPassword = number.ToString();
                            break;
                    }
                }
            }
        }
    }




}
