using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseKey : MonoBehaviour
{
    private List<string> house_Key_Strs_List;

    private MyData myData = null;
    private bool bWait = false;
    private bool bCursorChanged = false;
    private int talkCount = 0;

    private FPP_ObjScript fpp_Obj = null;

    private void Awake()
    {
        myData = GetComponent<MyData>();
        fpp_Obj = GetComponent<FPP_ObjScript>();

        house_Key_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.HOUSE_KEY_STRS);
    }

    private void OnMouseDown()
    {
        Inventory.instance.AddItem(myData.myData);

        FPP_Manager.Instance.OnOffText(true);
        FindTalk();
    }


    private void OnMouseEnter()
    {
        if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 3.0f)
            fpp_Obj.OnOutline(eSight.DOWN);

        if(!bCursorChanged)
        {
            bCursorChanged = true;

            FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[1]); // 손바닥
        }
    }

    private void OnMouseExit()
    {
        fpp_Obj.OffOutline();

        bCursorChanged = false;
        FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[0], false); // 손바닥
    }

    private void FindTalk()
    {
        FPP_Manager.Instance.GetMove().bObject = true;

        bWait = true;

        StartCoroutine(FastTalk());

        switch (talkCount)
        {
            case 0:
                {
                    FPP_Manager.Instance.FindObjectTalk(house_Key_Strs_List[0], () =>
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
            default:
                {
                    FPP_Manager.Instance.FindObjectTalk(house_Key_Strs_List[1], () =>
                    {

                        StartCoroutine(NextGot());
                    });
                }
                break;
        }
    }

    private IEnumerator NextTalk()
    {
        while (bWait)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                talkCount++;
                bWait = false;
                Time.timeScale = 1.0f;
            }

            yield return new WaitUntil(() => true);
        }

        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        FindTalk();
    }

    private IEnumerator NextGot()
    {
        while (bWait)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                bWait = false;
                Time.timeScale = 1.0f;
            }

            yield return new WaitUntil(() => true);
        }

        for(int i = 0; i < GameManager.Instance.Book.checkList_List.Count; i++)
        {
            if(GameManager.Instance.Book.checkList_List[i].GetComponentInChildren<Text>().text == CheckLists.FPP_CHECKLIST_STRS[2])
            {
                GameManager.Instance.Book.checkList_List[i].GetComponent<Toggle>().isOn = true;
            }
        }

        FPP_Manager.Instance.GetMove().bObject = false;
        FPP_Manager.Instance.OnOffText(false);
    }

    private IEnumerator FastTalk()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 5.0f;
                break;
            }

            yield return new WaitUntil(() => true);
        }
    }
}
