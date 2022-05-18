using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPP_Wife
{
    public class Book : MonoBehaviour
    {
        private List<string> book_Strs_List;

        private int talkCount = 0;
        private bool bWait = false;
        private bool bCursorChanged = false;

        public eSight item_Sight = eSight.MIDDLE;
        private FPP_ObjScript fpp_Obj = null;

        private void Awake()
        {
            fpp_Obj = GetComponent<FPP_ObjScript>();

            book_Strs_List = FPP_Strs.GetStringArrToList(FPP_Strs.BOOK_STRS);
        }

        private void OnMouseDown()
        {
            if (item_Sight == FPP_Manager.Instance.GetMove().curSight && Vector3.Distance(transform.position, Camera.main.transform.position) <= 2.0f && !FPP_Manager.Instance.GetMove().bObject)
            {
                FindTalk();
            }
        }

        private void OnMouseEnter()
        {
            if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 2.0f)
            {
                fpp_Obj.OnOutline(item_Sight);

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

            switch (talkCount)
            {
                case 0:
                case 1:
                    {
                        FPP_Manager.Instance.FindObjectTalk(book_Strs_List[0], () =>
                        {
                            StartCoroutine(NextTalk());
                        });
                    }
                    break;
                default:
                    {
                        FPP_Manager.Instance.FindObjectTalk(book_Strs_List[1], () =>
                        {
                            StartCoroutine(NextGot());
                        });
                        return;
                    }
            }

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

        private IEnumerator NextTalk()
        {
            while (bWait)
            {
                if (Input.GetKeyDown(KeyCode.Space))
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
            while (bWait)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    bWait = false;
                }

                yield return new WaitUntil(() => true);
            }

            talkCount = 0;

            Time.timeScale = 1.0f;


            FPP_Manager.Instance.GetMove().bObject = false;
            FPP_Manager.Instance.OnOffText(false);
        }
    }
}