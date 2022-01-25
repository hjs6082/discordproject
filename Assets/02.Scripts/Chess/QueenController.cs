using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QueenPuzzle
{
    public class QueenController : MonoBehaviour
    {
        private RaycastHit hit;
        private RaycastHit boardHit;
        private RaycastHit[] hits;
        private Ray ray;
        private AudioSource dropQueen;
        private GameObject queen;
        public Queen[] queensArr;

        private Vector3 beforePos;

        bool isClicked = false; // 누르고 있는지
        bool bClear = false; // 클리어했는지
        bool bChecking = false; // 체크 중 인지

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (!isClicked)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Physics.Raycast(ray, out hit, 30f);

                    if (hit.transform.CompareTag("QUEEN"))
                    {
                        if (queen == null)
                        {
                            queen = hit.transform.gameObject;
                            dropQueen = queen.GetComponent<AudioSource>();
                            if (dropQueen.isPlaying) dropQueen.Stop();
                        }
                        beforePos = hit.transform.position;
                        //Debug.Log(string.Format("{0}, {1}, {2}", beforePos.x, beforePos.y, beforePos.z));
                        isClicked = true;
                    }
                }

                if (queen != null)
                {
                    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    hits = Physics.RaycastAll(ray, 30f);

                    foreach (RaycastHit rayHit in hits)
                    {
                        if (rayHit.transform.tag.Equals("BOARD"))
                        {
                            boardHit = rayHit;
                        }
                    }

                    queen.transform.position = new Vector3(boardHit.point.x, 0.3f, boardHit.point.z);
                }
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (queen != null)
                {
                    Vector3 boardHitPos = boardHit.transform.position;

                    queen.transform.position = (queen.GetComponent<Queen>().bQueenOn) ? beforePos : new Vector3(boardHitPos.x, 0, boardHitPos.z);
                    dropQueen.Play();
                    if(!bChecking) StartCoroutine(ClearCheck());

                    queen = null;
                }
                isClicked = false;
            }
        }

        public void Clear(bool _bClear)
        {
            if (_bClear)
            {
                Debug.Log("Clear");
                // 클리어 때 실행
            }
            else Debug.Log("No");
        }

        IEnumerator ClearCheck()
        {
            bChecking = true;
            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < queensArr.Length; i++)
            {
                bClear = queensArr[i].CheckQueen();
                //Debug.Log(i + " " + queensArr[i].CheckQueen());
                if (!bClear) break;
            }
            Clear(bClear);
            bChecking = false;
        }
    }
}
