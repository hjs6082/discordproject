using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordScript : MonoBehaviour
{
    public Text passwordText;
    public bool isCheck;

    private string clearText = "2048";
    
    public void KeyPadClick(Text myText)
    {
        if(passwordText.text.Length <= 3)
        {
            passwordText.text += myText.text;
        }
    }

    public void KeyPadClear()
    {
        passwordText.text = "";
    }

    public void KeyPadCheck(GameObject ptObj)
    {
        if (!isCheck)
        {
            if (passwordText.text == clearText)
            {
                StartCoroutine(ClearTextPlay(ptObj));
            }
            else
            {
                StartCoroutine(WrongTextPlay());
            }
        }
    }

    public void KeyPadBackSpace()
    {
        if (!isCheck)
        {
            if (passwordText.text.Length >= 1)
            {
                passwordText.text = passwordText.text.Substring(0, passwordText.text.Length - 1);
            }
        }
    }

    IEnumerator ClearTextPlay(GameObject pt)
    {
        isCheck = true;
        passwordText.text = "OK";
        yield return new WaitForSeconds(1f);
        isCheck = false;
        pt.GetComponent<PasswordOpen>().PasswordDown();
        pt.GetComponent<PasswordOpen>().puzzleClear = true;
    }

    IEnumerator WrongTextPlay()
    {
        isCheck = true;
        string backText = passwordText.text;
        passwordText.text = "Wrong";
        yield return new WaitForSeconds(1.5f);
        passwordText.text = backText;
        isCheck = false;
    }
}
