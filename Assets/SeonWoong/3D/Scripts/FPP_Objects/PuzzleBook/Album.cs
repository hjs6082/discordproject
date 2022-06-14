using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wife_Scene
{
    public class Album : MonoBehaviour
    {
        private List<string> album_Strs_List;

        private StringBuilder check_SB = new StringBuilder();
        private int talkCount = 0;
        private bool bWait = false;
        private bool bCursorChanged = false;

        private FPP_Outline fpp_Outline = null;

        private void Awake()
        {
            fpp_Outline = GetComponent<FPP_Outline>();

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
                GameManager.Instance.book.GetComponent<Book_Main>().checkList_List[0].GetComponent<Toggle>().isOn = true;
                FPP_Manager.Instance.houseKey.SetActive(true);
            }
        }

        private void OnMouseEnter()
        {
            if (Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 2.0f)
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
            FPP_MouseCursor.InitCursor();
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

            FPP_Manager.Instance.GetMove().bObject = false;
            FPP_Manager.Instance.OnOffText(false);
            
            GameManager.Instance.book
            .SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
