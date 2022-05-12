using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPP_Wife
{
    public class Album : MonoBehaviour
    {
        private const string CHECK_STR = "...";
        private const string FIND_ALBUM_STR = "좋아, 앨범을 찾았다!";
        private const string GOT_ALBUM_STR  = "이제부터 앨범이 사용가능하다.";
        private const string EXPLAIN_ALBUM_STR = "현재 정보는 L, 사진첩은 P\n맵 정보는 M, 옵션은 ESC로 열 수 있다";

        private StringBuilder check_SB = new StringBuilder();
        private int talkCount = 0;
        private bool bWait = false;

        private void Start()
        {
            check_SB.Clear();
            check_SB.Append("...");
        }

        private void OnMouseDown()
        {
            if(Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position) <= 2.0f)
            {
                FindTalk();
            }
        }

        private void FindTalk()
        {
            FPP_Manager.Instance.GetMove().bObject = true;

            bWait = true;

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
                {
                    FPP_Manager.Instance.FindObjectTalk(FIND_ALBUM_STR, () => 
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
                case 3:
                {
                    FPP_Manager.Instance.FindObjectTalk(GOT_ALBUM_STR, () => 
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
                default:
                {
                    FPP_Manager.Instance.FindObjectTalk(EXPLAIN_ALBUM_STR, () => 
                    {
                        StartCoroutine(NextGot());
                    });

                    return;
                }
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

            FPP_Manager.Instance.GetMove().bObject = false;
            FPP_Manager.Instance.OnOffText(false);
            GameManager.Instance.Book.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
