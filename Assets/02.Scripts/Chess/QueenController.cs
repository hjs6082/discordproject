using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace QueenPuzzle
{
    public class QueenController : MonoBehaviour
    {
        public ChangeCam changeCam;

        private RaycastHit hit;
        private RaycastHit boardHit;
        private RaycastHit[] hits;
        private Ray ray;
        private AudioSource dropQueen;
        private GameObject queen;
        public Queen[] queensArr;
        public GameObject clearText;

        private Vector3 beforePos;

        bool isClicked = false; // 누르고 있는지
        bool bClear = false; // 클리어했는지
        bool bChecking = false; // 체크 중 인지

        private void Awake()
        {
            clearText.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            clearText.GetComponentInChildren<Text>().color = new Color(204f / 255f, 204f / 255f, 204f / 255f, 0);
            clearText.SetActive(false);
        }

        private void Update()
        {
            if (GameManager.Instance.isPuzzle)
            {
                if (Input.GetMouseButton(0))
                {
                    if (!isClicked)
                    {
                        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        Physics.Raycast(ray, out hit, 30f);
                        //Debug.DrawRay(ray.origin, ray.direction, Color.blue, 0.5f);

                        if (hit.transform != null)
                        {
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

                        queen.transform.position = new Vector3(boardHit.point.x, boardHit.transform.position.y + 0.3f, boardHit.point.z);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    if (queen != null)
                    {
                        if (boardHit.transform != null)
                        {
                            Vector3 boardHitPos = boardHit.transform.position;
                            queen.transform.position = (queen.GetComponent<Queen>().bQueenOn) ? beforePos : new Vector3(boardHitPos.x, boardHit.transform.position.y, boardHitPos.z);
                            dropQueen.Play();
                            if (!bChecking) StartCoroutine(ClearCheck());
                        }


                        queen = null;
                    }
                    isClicked = false;
                }
            }
        }

        public void Clear(bool _bClear)
        {
            if (_bClear)
            {
                // clearText.SetActive(true);

                // if (AudioManager.Instance != null)
                //     AudioManager.Instance.ClearSound();

                // clearText.GetComponentInChildren<Text>().DOFade(1f, 2f).OnComplete(() =>
                // {
                //     clearText.GetComponentInChildren<Text>().DOFade(0f, 2f);
                // });
                // clearText.GetComponent<Image>().DOFade(150f / 255f, 2f).OnComplete(() =>
                // {
                //     clearText.GetComponent<Image>().DOFade(0f, 2f).OnComplete(() =>
                //     {
                //         clearText.SetActive(false);
                //         //LoadScene.LoadingScene("MoveScene");
                //         changeCam.MoveCam();
                //         GameManager.Instance.isPuzzle = false;
                //         GameManager.Instance.bChessClear = true;
                //         GameManager.Instance.SavePuzzle();
                //     });
                // });

                Debug.Log(transform.position.x);
                this.transform.DOMoveX(-10.6f, 1.0f);
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
