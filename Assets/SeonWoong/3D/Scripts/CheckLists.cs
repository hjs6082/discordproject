using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheckLists : MonoBehaviour
{
    
    public static void AddCheckList(string _str)
    {
        GameManager.Instance.book.SetActive(true);

        GameObject check = Instantiate(GameManager.Instance.check_Prefab, GameManager.Instance.book.GetComponent<Book_Main>().checkList_Parent);
        check.GetComponent<TextMeshProUGUI>().text = _str;
        check.GetComponentInChildren<Toggle>().isOn = false;

        GameManager.Instance.book.GetComponent<Book_Main>().checkList_List.Add(check);

        GameManager.Instance.book.SetActive(false);
    }
}
