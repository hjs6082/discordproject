using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPP_Wife
{
    public class Book : MonoBehaviour
    {
        private const string FIND_BOOK_STR = "이건 앨범이 아닌 듯 하다.";

        private int  talkCount = 0;
        private bool bWait = false;
        
        private void OnMouseDown()
        {
            if(Vector3.Distance(transform.position, Camera.main.transform.position) <= 2.0f)
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
                    FPP_Manager.Instance.FindObjectTalk("...", () => 
                    {
                        StartCoroutine(NextTalk());
                    });
                }
                break;
                default:
                {
                    FPP_Manager.Instance.FindObjectTalk(FIND_BOOK_STR, () => 
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

            talkCount = 0;

            FPP_Manager.Instance.GetMove().bObject = false;
            FPP_Manager.Instance.OnOffText(false);
        }
    }
}