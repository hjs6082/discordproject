using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

            

            FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[1]); // 손바닥
        }
    }

    private void OnMouseExit()
    {
        bCursorChanged = false;
        FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[0], false); // 손바닥
    }

    private void OnMouseDown()
    {
        FPP_Manager.Instance.OnOffText(true);

        for(int i = 0; i < GameManager.Instance.Book.checkList_List.Count; i++)
        {
            string text = GameManager.Instance.Book.checkList_List[i].GetComponentInChildren<Text>().text;

            if(text == "서랍에서 사진 찾기" || text == "서랍 열쇠 찾기")
            {
                GameManager.Instance.Book.checkList_List[i].GetComponent<Toggle>().isOn = true;
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

        CheckLists.AddCheckList(CheckLists.FPP_CHECKLIST_STRS[3]);

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
