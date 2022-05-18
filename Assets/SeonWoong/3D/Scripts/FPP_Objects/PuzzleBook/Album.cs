using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPP_Wife
{
    public class Album : MonoBehaviour
    {
        private List<string> album_Strs_List;

        private StringBuilder check_SB = new StringBuilder();
        private int talkCount = 0;
        private bool bWait = false;
        private bool bCursorChanged = false;

        private FPP_ObjScript fpp_Obj = null;

        private void Awake()
        {
            fpp_Obj = GetComponent<FPP_ObjScript>();

            album_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.ALBUM_STRS);
        }

        private void Start()
        {
            check_SB.Clear();
            check_SB.Append(album_Strs_List[0]);
        }

        private void OnMouseDown()
        {
            if(Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 2.0f && !FPP_Manager.Instance.GetMove().bObject)
            {
                FindTalk();
                GameManager.Instance.Book.checkList_List[0].GetComponent<Toggle>().isOn = true;
                FPP_Manager.Instance.houseKey.SetActive(true);
            }
        }

        private void OnMouseEnter()
        {
            if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 2.0f)
            {
                fpp_Obj.OnOutline(eSight.MIDDLE);

                if (!bCursorChanged)
                {
                    bCursorChanged = true;

                    FPP_MouseCursor.ChangeCursor(FPP_Manager.Instance.cursor_Textures[3]); // 손바닥
                }
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

            switch(talkCount)
            {
                case 0:
                case 1:
                {
                    FPP_Manager.Instance.FindObjectTalk(check_SB.ToString(), () => 
                    {
                        check_SB.Append("!");
                        StartCoroutine(NextTalk());
                    });
                }
                break;
                case 2:
                case 3:
                {
                    FPP_Manager.Instance.FindObjectTalk(album_Strs_List[talkCount - 1], () => 
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
                default:
                {
                    FPP_Manager.Instance.FindObjectTalk(album_Strs_List[talkCount - 1], () => 
                    {
                        StartCoroutine(NextGot());
                    });

                    return;
                }
            }
        }

        private IEnumerator FastTalk()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 5.0f;
                break;
            }

            yield return new WaitUntil(() => true);
        }
    }

        private IEnumerator NextTalk()
        {
            while(bWait)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    talkCount++;
                    bWait = false;
                }

                yield return new WaitUntil(() => true);
            }

            Time.timeScale = 1.0f;
            FindTalk();
        }

        private IEnumerator NextGot()
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

            CheckLists.AddCheckList(CheckLists.FPP_CHECKLIST_STRS[1]);

            FPP_Manager.Instance.GetMove().bObject = false;
            FPP_Manager.Instance.OnOffText(false);
            GameManager.Instance.Book.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
