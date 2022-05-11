using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnglishPassword : MonoBehaviour
{
    public static EnglishPassword instance;
    public TextMeshProUGUI englishPassword;
    public bool isCheck;
    public bool isAnswer;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(englishPassword.text.Length == 4)
        {
            AnswerCheck();
            if(isAnswer)
            {
                Debug.Log("Open");
            }
            else if(!isAnswer)
            {
                isCheck = true;
                StartCoroutine(isWrong());
            }
        }
    }

    public void AnswerCheck()
    {
        if(englishPassword.text =="snow")
        {
            isAnswer = true;
        }
    }

    IEnumerator isWrong()
    {
        englishPassword.text = "wrong";
        yield return new WaitForSeconds(1f);
        englishPassword.text  = "";
        isCheck = false;
    }
}
