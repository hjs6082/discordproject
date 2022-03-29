// using System;
// using System.Linq;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using DG.Tweening;
// using UnityEngine.SceneManagement;

// namespace Dialog_Past
// {
//     public class Dialog_Controll : MonoBehaviour
//     {
//         private List<string> manStory_Strs = new List<string>();
//         private List<string> womanStory_Strs = new List<string>();
//         private List<string> manSuccess_Strs = new List<string>();
//         private List<string> womanSuccess_Strs = new List<string>();
//         private List<string> manFail_Strs = new List<string>();
//         private List<string> womanFail_Strs = new List<string>();
//         private List<string> space_Strs = new List<string>();

//         private void Awake()
//         {
//             InitStrs();
//         }

//         private void Start()
//         {

//         }

//         private void Update()
//         {

//         }

//         private void InitStrs()
//         {
//             manStory_Strs = Dialog_Strs.manStoryArr.ToList();
//             womanStory_Strs = Dialog_Strs.womanStoryArr.ToList();
//             manSuccess_Strs = Dialog_Strs.manSuccessArr.ToList();
//             womanSuccess_Strs = Dialog_Strs.womanSuccessArr.ToList();
//             manFail_Strs = Dialog_Strs.manFailArr.ToList();
//             womanFail_Strs = Dialog_Strs.womanFailArr.ToList();
//             space_Strs = Dialog_Strs.spaceStrsArr.ToList();
//         }

//         public void Next(eIndex type, int talkVal)
//         {

//         }

//         public void Talking(GameObject dialog, string str) // 대화
//         {

//         }

//         public void Damaged(RectTransform obj)
//         {

//         }

//         public void SkipSpeech(eIndex type)
//         {

//         }
//     }
// }
