using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Photo : MonoBehaviour
{
    private List<string> photo_Strs_List;

    [SerializeField] private Sprite photo_Sprite = null;

    private bool bWait = false;
    private bool bCursorChanged = false;
    private int  talkCount = 0;

    private void Awake()
    {
        photo_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.PHOTO_STRS);
    }

    private void OnMouseEnter()
    {
        if(!bCursorChanged)
        {
            bCursorChanged = true;

            

        }
    }

    private void OnMouseExit()
    {
        bCursorChanged = false;
        FPP_MouseCursor.InitCursor();
    }

    private void OnMouseDown()
    {
        Book_Main bm = GameManager.Instance.book.GetComponent<Book_Main>();

        FPP_Manager.Instance.OnOffText(true);

        for (int i = 0; i < GameManager.Instance.GetBook_Main().checkList_List.Count; i++)
        {
            GameObject checkList = GameManager.Instance.GetBook_Main().checkList_List[i];
            if (checkList.GetComponent<TextMeshProUGUI>().text == FPP_Manager.FPP_CHECKLIST_STRS[1])
            {
                checkList.GetComponentInChildren<Toggle>().isOn = true;
            }
        }

        FindTalk();
    }

    private void FindTalk()
    {
        FPP_Manager.Instance.GetMove().bObject = true;

        bWait = true;

        StartCoroutine(FastTalk());

        switch (talkCount)
        {
            case 0:
            case 1:
                {
                    FPP_Manager.Instance.FindObjectTalk(photo_Strs_List[talkCount], () =>
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
            default:
                {
                    StartCoroutine(NextGot());
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

        if(talkCount == 2)
        {
            GameManager.Instance.AddPhoto(photo_Sprite);
            GameManager.Instance.BookCtrl(ePage.ALBUM);
        }

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

        GameManager.Instance.BookCtrl(ePage.ALBUM);
        
        FPP_Manager.Instance.GetMove().bObject = false;
        FPP_Manager.Instance.OnOffText(false);
        this.gameObject.SetActive(false);
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
