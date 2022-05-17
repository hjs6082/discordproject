using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    private List<string> photo_Strs_List;

    [SerializeField] private Sprite photo_Sprite = null;

    private bool bWait = false;
    private bool bCursorChanged = false;
    private int  talkCount = 0;

    private void Awake()
    {
        photo_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.ALBUM_STRS);
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

        if(GameManager.Instance.Book.gameObject.activeSelf)
        {
            GameManager.Instance.AddPhoto(photo_Sprite);
        }
        else
        {
            Inventory.instance.AddItem(GetComponent<MyData>().myData);
        }

        StartCoroutine(FastTalk());
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
                {
                    FPP_Manager.Instance.FindObjectTalk(photo_Strs_List[talkCount], () =>
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
            default:
                {
                    FPP_Manager.Instance.FindObjectTalk(photo_Strs_List[talkCount], () =>
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
