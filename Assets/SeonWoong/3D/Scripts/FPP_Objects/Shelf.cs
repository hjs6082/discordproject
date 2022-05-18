using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Shelf : MonoBehaviour
{
    private const float CLOSE_POS_Z = 0.58f;
    private const float OPEN_POS_Z  = 1.0f;

    public List<string> shelf_Strs_List;

    public eSight item_Sight = eSight.MIDDLE;
    public bool isLocked = false;
    private bool isOpen = false;
    private bool bCursorChanged = false;
    private bool bWait = false;

    private FPP_ObjScript fpp_Obj = null;

    private void Awake()
    {
        fpp_Obj = GetComponent<FPP_ObjScript>();

        shelf_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.SHELF_STRS);
    }

    private void OnMouseDown()
    {
        if (item_Sight == FPP_Manager.Instance.GetMove().curSight && GameManager.Instance.Book.gameObject.activeSelf)
        {
            if (isLocked && Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.0f)
            {
                for (int i = 0; i < Inventory.instance.items.Count; i++)
                {
                    if (Inventory.instance.items[i].itemName == "서랍 키")
                    {
                        isLocked = false;
                        break;
                    }
                }

                if (isLocked)
                {
                    FPP_Manager.Instance.OnOffText(true);
                    StartCoroutine(FastTalk());
                    FPP_Manager.Instance.FindObjectTalk(shelf_Strs_List[0], () => 
                    {
                        bWait = true;
                        StartCoroutine(NextTalk());
                    });


                    return;
                }
            }

            float moveOffset = (isOpen) ? CLOSE_POS_Z : OPEN_POS_Z;

            MoveShelf(moveOffset);
        }

    }

    private void OnMouseEnter()
    {

        if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.0f && GameManager.Instance.Book.gameObject.activeSelf)
        {
            fpp_Obj.OnOutline(item_Sight);

            if (!bCursorChanged)
            {
                bCursorChanged = true;

                FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[2]); // 막대화살
            }
        }
    }

    private void OnMouseExit()
    {
        fpp_Obj.OffOutline();

        bCursorChanged = false;
        FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[0], false); // 손바닥
    }

    private void MoveShelf(float _dis)
    {
        isOpen = !isOpen;
        transform.DOLocalMoveZ(_dis, 0.5f)
        .SetEase(Ease.OutQuad);
    }


    private IEnumerator NextTalk()
        {
            while(bWait)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    bWait = false;
                }

                yield return new WaitUntil(() => true);
            }

            Time.timeScale = 1.0f;
            FPP_Manager.Instance.OnOffText(false);

            bool check = false;
            string str = CheckLists.FPP_CHECKLIST_STRS[2];
            for(int i = 0; i < GameManager.Instance.Book.checkList_List.Count; i++)
            {
                if(GameManager.Instance.Book.checkList_List[i].GetComponentInChildren<Text>().text == str) check = true;
            }

            if(!check)
            CheckLists.AddCheckList(CheckLists.FPP_CHECKLIST_STRS[2]);
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