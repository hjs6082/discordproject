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

    private FPP_Outline fpp_Outline = null;
    private Book_Main book_Main = null;

    private void Awake()
    {
        fpp_Outline = GetComponent<FPP_Outline>();
        book_Main = GameManager.Instance.book.GetComponent<Book_Main>();

        shelf_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.SHELF_STRS);
    }

    private void OnMouseDown()
    {
        if (book_Main.gameObject.activeSelf)
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
        if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 1.0f && GameManager.Instance.book.activeSelf)
        {
            fpp_Outline.OnOutline();

            if (!bCursorChanged)
            {
                bCursorChanged = true;

            }
        }
    }

    private void OnMouseExit()
    {
        fpp_Outline.OffOutline();

        bCursorChanged = false;
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
