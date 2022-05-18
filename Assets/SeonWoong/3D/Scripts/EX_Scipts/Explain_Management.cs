using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Explain_Management : MonoBehaviour
{
    private const string HUSBAND_STR = "게임 속";
    private const string WIFE_STR = "집 안";

    enum eExplain
    {   
        HUSBAND,
        WIFE
    };

    public  GameObject[] explain_Pages;
    private eExplain     curExplain = eExplain.HUSBAND;
    private Text explainPageText = null;

    private void Awake()
    {
        explainPageText = GetComponent<Text>();

        ChangeExplain((int)curExplain);
    }

    public void Next()
    {
        int curExplainInt = (int)curExplain;
        curExplainInt++;

        if(curExplainInt > 1)
        {
            curExplainInt = 0;
        }

        ChangeExplain(curExplainInt);
    }

    public void Prev()
    {
        int curExplainInt = (int)curExplain;
        curExplainInt--;

        if(curExplainInt < 0)
        {
            curExplainInt = 1;
        }

        ChangeExplain(curExplainInt);
    }

    private void ChangeExplain(int _eExplainInt)
    {
        for(int i = 0; i < explain_Pages.Length; i++)
        {
            explain_Pages[i].SetActive(false);
        }

        curExplain = (eExplain)_eExplainInt;
        explainPageText.text = (curExplain == eExplain.HUSBAND) ? HUSBAND_STR : WIFE_STR;
        explain_Pages[_eExplainInt].SetActive(true);
    }
}
