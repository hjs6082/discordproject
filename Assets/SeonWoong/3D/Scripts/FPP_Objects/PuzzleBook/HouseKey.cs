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

    private FPP_Outline fpp_Outline = null;

    private void Awake()
    {
        myData = GetComponent<MyData>();
        fpp_Outline = GetComponent<FPP_Outline>();

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
            fpp_Outline.OnOutline();

        if(!bCursorChanged)
        {
            bCursorChanged = true;

        }
    }

    private void OnMouseExit()
    {
        fpp_Outline.OffOutline();

        bCursorChanged = false;
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

        Book_Main bm = GameManager.Instance.book.GetComponent<Book_Main>();

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
