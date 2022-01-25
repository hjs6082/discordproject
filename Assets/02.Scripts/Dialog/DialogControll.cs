using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialog
{
    public enum eIndex
    {
        MAN,
        WOMAN,
        FAIRY,
        MAN_Story
    }

    public class DialogControll : MonoBehaviour
    {
        private Dictionary<KeyCode, int> KeyMapDic = new Dictionary<KeyCode, int>()
        {
            {KeyCode.Q, 0},
            {KeyCode.W, 1},
            {KeyCode.E, 2},
            {KeyCode.R, 3}
        };

        private Color SetColor(float r, float g, float b, float a)
        {
            return new Color(r/255f, g/255f, b/255f, a/255f);
        }

        public GameObject[] Dialogs; // 다이얼로그
        public Image[] CharacterImages; // 캐릭터 이미지
        private Dictionary<GameObject, GameObject> SpeechArrowDic = new Dictionary<GameObject, GameObject>();

        [Header("대사 리스트")]
        public List<string> manStrList = new List<string>();
        public List<string> womanStrList = new List<string>();
        public List<string> fairyStrList = new List<string>();
        public List<string> manStoryStrList = new List<string>();
        private Dictionary<eIndex, List<string>> charStrsDic = new Dictionary<eIndex, List<string>>();

        private eIndex[] OrderList = {
            eIndex.FAIRY,
            eIndex.MAN_Story,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.MAN,
            eIndex.WOMAN,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.MAN,
            eIndex.WOMAN,
            eIndex.MAN,
            eIndex.WOMAN,
            eIndex.MAN,
            eIndex.WOMAN, // 외딴 섬에나 떨어져
            eIndex.MAN_Story,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY
        };
        private int curOrder = 0; // 현재 몇 번째 순서인지
        private int[] talkVal = { 0, 0, 0 }; // 몇 번째 말하는지 (MAN, WOMAN, FAIRY 순)

        public bool isPlayer = false; // 플레이어가 조작할 차례인지
        public bool isTalking = false; // 캐릭터가 말하는 중인지
        
        private void Awake()
        {
            // 대사 리스트에 대사 추가
            manStrList = DialogStrs.manStrsArr.ToList();
            for(int i = 0; i < 4; i++)
            {
                string str = DialogStrs.womanStrsArr[i];
                womanStrList.Add(str);
            }
            fairyStrList = DialogStrs.fairyStrsArr.ToList();
            manStoryStrList = DialogStrs.manStoryArr.ToList();

            charStrsDic.Add(eIndex.MAN, manStrList);
            charStrsDic.Add(eIndex.WOMAN, womanStrList);
            charStrsDic.Add(eIndex.FAIRY, fairyStrList);
            charStrsDic.Add(eIndex.MAN_Story, manStoryStrList);

            for (int i = 0; i < Dialogs.Length; i++)
            {
                SpeechArrowDic.Add(Dialogs[i], Dialogs[i].transform.Find("Arrow").gameObject);
            }
        }

        private void Start()
        {
            eIndex type = OrderList[curOrder];
            Next(type, talkVal[(int)type]);
        }

        private void Update()
        {
            if(!isTalking)
            {
                if(isPlayer) // 플레이어 조작
                {
                    if(Input.anyKey)
                    {
                        foreach(var key in KeyMapDic)
                        {
                            if(Input.GetKeyDown(key.Key))
                            {
                                eIndex type = OrderList[curOrder];
                                Next(type, key.Value);
                            }
                        }
                    }
                }
                else // NPC 조작
                {
                    if(Input.GetKeyDown(KeyCode.Space))
                    {
                        eIndex type = OrderList[curOrder];
                        int iType = (int)type % 3;

                        Debug.Log($"{type}, {iType}");

                        Next(type, talkVal[iType]);
                    }
                }
            }
        }

        private GameObject InitDialog(int type)
        {
            GameObject dialog = null;

            for(int i = 0; i < Dialogs.Length; i++)
            {
                bool IsOn = (type == i) ? true : false;
                Dialogs[i].SetActive(IsOn);

                CharacterImages[i].color = (IsOn) ? SetColor(255f, 255f, 255f, 255f) : SetColor(120f, 120f, 120f, 255f);
                Debug.Log(CharacterImages[i].color);
                
                if(dialog == null)
                {
                    dialog = (IsOn) ? Dialogs[i] : null;
                }
            }

            return dialog;
        }

        public void Next(eIndex type, int talkVal)
        {
            isTalking = true;

            int iType = (int)type % 3;

            GameObject dialog = InitDialog(iType);
            string str = charStrsDic[type][talkVal];
            this.talkVal[iType]++;

            Talking(dialog, str);
        }

        public void Talking(GameObject dialog, string str) // 대화
        {
            Text dialogText = dialog.GetComponentInChildren<Text>();
            float duration = (float)str.Length / 6.0f; 

            SpeechArrowDic[dialog].SetActive(false);
            dialogText.text = "";

            dialogText.DOText(str, duration)
            .OnComplete(() => 
            {
                if(OrderList[curOrder + 1] == eIndex.MAN) { isPlayer = true; } 
                else { isPlayer = false; }

                if(str == "외딴 섬에나 떨어져!")
                {
                    // TODO : 씬 변경 혹은 화면 전환
                    Debug.Log("화면 전환 ~~");
                }

                isTalking = false;
                curOrder++;
                SpeechArrowDic[dialog].SetActive(true);
            });
        }
    }
}
