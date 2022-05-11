using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class EnglishPassword : MonoBehaviour
{
    [SerializeField]
    private GameObject doorObj;

    public static EnglishPassword instance;
    public TextMeshProUGUI englishPassword;
    public bool isCheck;
    public bool isAnswer;
    public bool isClear;

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
                isClear = true;
                englishPassword.text =  "clear";
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

    public void DoorOpen()
    {
        doorObj.transform.DOLocalRotate(new Vector3(-90, 0, 90), 1.5f);
    }
}
