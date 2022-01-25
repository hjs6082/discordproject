// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using DG.Tweening;

// namespace Dialog
// {
//     public class DialogControll__ : MonoBehaviour
//     {
//         private List<string> manStrsList = new List<string>();
//         private List<string> womanStrsList = new List<string>();
//         private List<string> fairyStrsList = new List<string>();
//         private List<List<string>> charStrsList = new List<List<string>>();
//         private Dictionary<KeyCode, int> keysDic = new Dictionary<KeyCode, int>()
//         {
//             {KeyCode.Q, 0},
//             {KeyCode.W, 1},
//             {KeyCode.E, 2},
//             {KeyCode.R, 3}
//         };

//         private const int MAN_INDEX = 0, WOMAN_INDEX = 1, FAIRY_INDEX = 2;

//         public GameObject[] dialogArr;

//         private int manOrder = 0, womanOrder = 0, fairyOrder = 0;
//         private int[] talkOrdersArr = {
//             FAIRY_INDEX,
//             MAN_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX,
//             MAN_INDEX,
//             WOMAN_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX,
//             MAN_INDEX,
//             WOMAN_INDEX,
//             MAN_INDEX,
//             WOMAN_INDEX,
//             MAN_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX,
//             FAIRY_INDEX
//         };
//         private int talkOrder = 0;
//         public bool bTalking = false;

//         private void Awake()
//         {
//             // 대사 추가
//             RandomAttackNum(DialogStrs.attackArr, manStrsList);
//             RandomAttackNum(DialogStrs.attackArr, womanStrsList);
//             for (int i = 0; i < DialogStrs.fairyStrsArr.Length; i++)
//             {
//                 fairyStrsList.Add(DialogStrs.fairyStrsArr[i]);
//             }
//             charStrsList.Add(manStrsList);
//             charStrsList.Add(womanStrsList);
//             charStrsList.Add(fairyStrsList);
//         }

//         private void RandomAttackNum(string[] strs, List<string> charList)
//         {
//             List<string> list = new List<string>();
//             for (int i = 0; i < strs.Length; i++)
//             {
//                 list.Add(strs[i]);
//             }

//             for (int i = 0; i < 4; i++)
//             {
//                 int randStr = UnityEngine.Random.Range(0, list.Count);
//                 charList.Add(list[randStr]);
//                 list.Remove(list[randStr]);
//             }
//         }

//         public void Talking(int index, int order)
//         {
//             if (bTalking)
//             {
//                 GameObject textArrow = dialogArr[index].transform.Find("Arrow").gameObject;
//                 textArrow.SetActive(false);
//                 dialogArr[index].GetComponentInChildren<Text>().text = "";
//                 Text dialogText = dialogArr[index].GetComponentInChildren<Text>();
//                 List<string> strsArr = charStrsList[index];
//                 float duration = strsArr[order].Length / 5;


//                 dialogText.DOText(strsArr[order], duration)
//                 .OnComplete(() =>
//                 {
//                     switch (index)
//                     {
//                         case MAN_INDEX:
//                             StartCoroutine(TalkingDelay(0.5f));
//                             manOrder++;
//                             break;
//                         case WOMAN_INDEX:
//                             StartCoroutine(TalkingDelay(0.5f));
//                             womanOrder++;
//                             break;
//                         case FAIRY_INDEX:
//                             StartCoroutine(TalkingDelay(0.5f));
//                             fairyOrder++;
//                             break;
//                     }
//                     textArrow.SetActive(true);
//                 });
//             }
//         }

//         private void InitDialog(int index)
//         {
//             bTalking = true;

//             for (int i = 0; i < dialogArr.Length; i++)
//             {
//                 bool bCheck = (i == index) ? true : false;
//                 dialogArr[i].SetActive(bCheck);
//             }
//         }

//         private void Start()
//         {
//             NextTalk(talkOrdersArr[talkOrder]);
//         }

//         private void Update()
//         {
//             if (!bTalking)
//             {
//                 if (Input.anyKey)
//                 {
//                     Debug.Log(talkOrdersArr[talkOrder]);
//                     InitDialog(talkOrdersArr[talkOrder]);
//                     if (talkOrdersArr[talkOrder] == MAN_INDEX)
//                     {
//                         if (manOrder % 5 == 0)
//                         {
//                             GameObject textArrow = dialogArr[MAN_INDEX].transform.Find("Arrow").gameObject;
//                             textArrow.SetActive(false);
//                             dialogArr[MAN_INDEX].GetComponentInChildren<Text>().text = "";
//                             Text dialogText = dialogArr[MAN_INDEX].GetComponentInChildren<Text>();
//                             float duration = (float)DialogStrs.manStrsArr[manOrder / 5].Length / (float)5;

//                             dialogText.DOText(DialogStrs.manStrsArr[manOrder / 5], duration)
//                             .OnComplete(() =>
//                             {
//                                 StartCoroutine(TalkingDelay(0.5f));
//                                 manOrder++;
//                                 textArrow.SetActive(true);
//                             });
//                         }
//                         else
//                         {
//                             foreach (var key in keysDic)
//                             {
//                                 if (Input.GetKeyDown(key.Key))
//                                 {
//                                     Talking(MAN_INDEX, key.Value);
//                                 }
//                                 else
//                                 {
//                                     GameObject textArrow = dialogArr[FAIRY_INDEX].transform.Find("Arrow").gameObject;
//                                     textArrow.SetActive(false);
//                                     dialogArr[FAIRY_INDEX].GetComponentInChildren<Text>().text = "";
//                                     Text dialogText = dialogArr[FAIRY_INDEX].GetComponentInChildren<Text>();
//                                     float duration = 3f;

//                                     dialogText.DOText("Q W E R 중에서\n하나를 눌러\n공격해보세요~", duration)
//                                     .OnComplete(() =>
//                                     {
//                                         StartCoroutine(TalkingDelay(0.5f));
//                                         manOrder++;
//                                         textArrow.SetActive(true);
//                                     });
//                                 }
//                             }
//                         }
//                     }
//                     else
//                     {
//                         NextTalk(talkOrdersArr[talkOrder]);
//                     }
//                 }
//             }
//         }


//         private void NextTalk(int index)
//         {
//             switch (index)
//             {
//                 case WOMAN_INDEX:
//                     Talking(index, womanOrder);
//                     break;

//                 case FAIRY_INDEX:
//                     Talking(index, fairyOrder);
//                     break;

//                 default:
//                     break;
//             }
//         }
//         IEnumerator TalkingDelay(float duration)
//         {
//             yield return new WaitForSeconds(duration);

//             bTalking = false;
//             talkOrder++;
//         }
//     }
// }
