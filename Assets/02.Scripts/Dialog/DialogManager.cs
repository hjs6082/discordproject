using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public enum eIndex
    {
        MAN,
        WOMAN,
        FAIRY,
        MAN_Story
    }

    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance { get; private set; }

        public Color SetColor(float r, float g, float b, float a)
        {
            Color color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);

            return color;
        }

        private const string ARROW_TEXT = "를 눌러 넘기기 ->    ";

        private Dictionary<KeyCode, int> KeyMapDic = new Dictionary<KeyCode, int>()
        {
            {KeyCode.Q, 0},
            {KeyCode.W, 1},
            {KeyCode.E, 2},
            {KeyCode.R, 3}
        };

        private eIndex[] OrderList = {
            eIndex.FAIRY,
            eIndex.MAN_Story,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.MAN,
            eIndex.WOMAN,
            eIndex.FAIRY,
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
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY,
            eIndex.FAIRY
        };
        private eIndex currentIndex;
        private int totalOrder = 0;

        public ManDialogControl manCtrl;
        public WomanDialogControl womanCtrl;
        public FairyDialogControl fairyCtrl;

        private List<DialogControl_main> ctrlList = new List<DialogControl_main>();

        public CharacterVoice charVoice;

        public Text ManNameText;
        public Text WomanNameText;

        public Image background;
        public Sprite inGameImg;

        public bool isTalking = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            if (GameManager.Instance != null)
            {
                if (ManNameText != null) { ManNameText.text = GameManager.Instance.ManName; }
                if (WomanNameText != null) { WomanNameText.text = GameManager.Instance.WomanName; }
            }

            ctrlList.Add(manCtrl);
            ctrlList.Add(womanCtrl);
            ctrlList.Add(fairyCtrl);

            currentIndex = OrderList[totalOrder];
        }

        private void Start()
        {
            Talk();
        }

        private void Update()
        {
            if (totalOrder < OrderList.Length)
            {
                if (!isTalking)
                {
                    if (currentIndex == eIndex.MAN)
                    {
                        if (Input.anyKey)
                        {
                            foreach (var keys in KeyMapDic)
                            {
                                if (Input.GetKeyDown(keys.Key))
                                {
                                    int indexNum = (int)currentIndex % 3;

                                    isTalking = true;
                                    manCtrl.isPlayer = true;

                                    InitDialog(indexNum);

                                    manCtrl.Attack(keys.Value, charVoice.audioSource);

                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            Talk();
                            return;
                        }
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        int indexNum = (int)currentIndex % 3;

                        ctrlList[indexNum].SkipSpeech();

                        return;
                    }
                }
            }
            else
            {
                
            }            
        }

        private void Talk()
        {
            if(totalOrder == 17)
            {
                // 씬이동
                Debug.Log("화면 전환");


                //TODO : 눈 감았다 뜨는 애니메이션


                //LoadScene.LoadingScene("MoveScene");
                // 임시로 배경사진 바꾸기

                background.sprite = inGameImg;
                 
                ctrlList[1].gameObject.SetActive(false);
            }
            else if(totalOrder == 18)
            {
                ctrlList[0].gameObject.SetActive(false);
            }
            else if(totalOrder >= OrderList.Length - 1)
            {
                DialogManager.Instance.gameObject.SetActive(false);
                LoadScene.LoadingScene("MoveScene");
                return;
            }

            int indexNum = (int)currentIndex % 3;
            AudioSource source = charVoice.audioSource;

            isTalking = true;

            InitDialog(indexNum);

            if (totalOrder < OrderList.Length - 1)
            {
                if (OrderList[totalOrder + 1] == eIndex.MAN)
                {
                    womanCtrl.arrowText.GetComponent<Text>().text = "QWER" + ARROW_TEXT;
                    fairyCtrl.arrowText.GetComponent<Text>().text = "QWER" + ARROW_TEXT;
                }
                else
                {
                    womanCtrl.arrowText.GetComponent<Text>().text = "SPACE바" + ARROW_TEXT;
                    fairyCtrl.arrowText.GetComponent<Text>().text = "SPACE바" + ARROW_TEXT;
                }
            }

            ctrlList[indexNum].Next(source);

            if (totalOrder == OrderList.Length)
            {
                for (int i = 0; i < ctrlList.Count; i++)
                {
                    ctrlList[i].gameObject.SetActive(false);
                }
            }
        }

        public void NextOrder()
        {
            totalOrder++;

            if (totalOrder < OrderList.Length)
            {
                currentIndex = OrderList[totalOrder];
            }
        }

        private void InitDialog(int _indexNum)
        {
            if(_indexNum == 2)
            {
                ctrlList[2].gameObject.SetActive(true);
            }
            else
            {
                ctrlList[2].gameObject.SetActive(false);
            }
        }
    }
}
