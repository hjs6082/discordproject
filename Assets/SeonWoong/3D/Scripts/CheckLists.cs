using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLists : MonoBehaviour
{
    public static readonly string[] FPP_CHECKLIST_STRS = 
    {
        "책들 중 앨범 찾기",
        "서랍에서 사진 찾기",
        "서랍 열쇠 찾기",
        "컴퓨터로 다시 돌아가기"
    };

    public static void AddCheckList(string _str)
    {
        GameObject check = Instantiate(GameManager.Instance.check_Prefab, GameManager.Instance.Book.checkList_Parent);
        check.GetComponentInChildren<Text>().text = _str;
        check.GetComponent<Toggle>().isOn = false;

        GameManager.Instance.Book.checkList_List.Add(check);
    }
}
