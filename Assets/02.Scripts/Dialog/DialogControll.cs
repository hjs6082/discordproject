using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialog
{
    public class DialogControll : MonoBehaviour
    {
        private List<string[]> charStrsArrList = new List<string[]>()
        {
            DialogStrs.manStrsArr,
            DialogStrs.womanStrsArr,
            DialogStrs.fairyStrsArr
        };
        private Dictionary<KeyCode, int> keysDic = new Dictionary<KeyCode, int>()
        {
            {KeyCode.Q, 0},
            {KeyCode.W, 1},
            {KeyCode.E, 2},
            {KeyCode.R, 3}
        };

        private const int MAN_INDEX = 0, WOMAN_INDEX = 1, FAIRY_INDEX = 2;

        public GameObject[] dialogArr;

        private int womanOrder = 0, fairyOrder = 0;
        private int[] talkOrdersArr = { 2, 0, 1, 2, 2, 0, 1, 0, 1 };
        private int talkOrder = 0;
        public bool bTalking = false;

        public void Talking(int index, int order)
        {
            InitDialog(index);

            if (bTalking)
            {
                Sequence seq = DOTween.Sequence();

                GameObject dialog = dialogArr[index];
                dialogArr[index].GetComponentInChildren<Text>().text = "";
                Text dialogText = dialog.GetComponentInChildren<Text>();
                string[] strsArr = charStrsArrList[index];
                float duration = strsArr[order].Length % 10;


                seq.Append(dialogText.DOText(strsArr[order], duration))
                .AppendInterval(1f)
                .OnComplete(() =>
                {
                    bTalking = false;
                    talkOrder++;
                });
            }
        }

        private void InitDialog(int index)
        {
            bTalking = true;

            for (int i = 0; i < dialogArr.Length; i++)
            {
                bool bCheck = (i == index) ? true : false;
                dialogArr[i].SetActive(bCheck);
            }
        }

        private void Update()
        {
            if (!bTalking)
            {
                switch (talkOrdersArr[talkOrder])
                {
                    case MAN_INDEX:
                        if (Input.anyKey)
                        {
                            foreach (var key in keysDic)
                            {
                                if (Input.GetKeyDown(key.Key))
                                {
                                    Talking(MAN_INDEX, key.Value);
                                }
                            }
                        }
                        break;

                    case WOMAN_INDEX:
                        Talking(WOMAN_INDEX, womanOrder);
                        womanOrder++;
                        break;

                    case FAIRY_INDEX:
                        Talking(FAIRY_INDEX, fairyOrder);
                        fairyOrder++;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
