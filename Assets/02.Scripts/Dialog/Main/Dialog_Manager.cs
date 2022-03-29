using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialog
{
    public class Dialog_Manager : MonoBehaviour
    {
        public static Dialog_Manager Instance { get; private set; }
        public static Action<bool> damaged;

        public Dialog_Control dialog_Ctrl = null;
        public Dialog_Talk dialog_Talk = null;

        public CharacterVoice charVoice = null;

        public Image background = null;
        public Sprite inGameImg = null;
        public Image wife_Image = null;

        public PanelOnOff panelOnOff = null;
        public Button action_Button = null;
        public Transform button_Parent = null;
        private List<Button> action_List = new List<Button>();

        public GameObject[] hearts;
        public Transform[] heart_Parents;
        private List<GameObject> blue_Heart_List = new List<GameObject>();
        private List<GameObject> red_Heart_List = new List<GameObject>();

        public bool isTalking = false;
        public bool bExplainOnce = false;
        public bool bWin = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            dialog_Ctrl = GetComponent<Dialog_Control>();
            dialog_Talk = GetComponent<Dialog_Talk>();

            damaged += Damaged;
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                dialog_Talk.SetName(GameManager.Instance.ManName, GameManager.Instance.WomanName);
            }
            else
            {
                dialog_Talk.SetName("남편", "아내");
            }

            InitButtons();

            OnOffButtons(false);
        }

        private void Update()
        {

        }

        public void Damaged(bool _bWin)
        {
            bWin = _bWin;
            OnOffButtons(true);
            panelOnOff.OnOff(bWin, null);
        }

        public void AddHeart()
        {
            wife_Image.rectTransform.DOShakeAnchorPos(0.5f, 50, 10);
            background.rectTransform.DOShakeAnchorPos(0.5f, 100, 15).OnComplete(() =>
            {
                int heart_Index = (bWin) ? 0 : 1;

                GameObject heart = Instantiate(hearts[heart_Index], heart_Parents[heart_Index]);

                switch (heart_Index)
                {
                    case 0:
                        blue_Heart_List.Add(heart);
                        break;
                    case 1:
                        red_Heart_List.Add(heart);
                        break;
                    default:
                        break;
                }
            });
        }

        private void InitButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                int index = i;
                Button button = Instantiate(action_Button, button_Parent);
                button.onClick.AddListener(() =>
                {
                    panelOnOff.MiniGame(index);

                    if (dialog_Talk.space_Strs.Count > 0)
                    {
                        dialog_Talk.Space_Talk(3.5f, null);
                    }
                });
                action_List.Add(button);
            }
        }

        public void OnOffButtons(bool _bInteractable)
        {
            for (int i = 0; i < action_List.Count; i++)
            {
                action_List[i].interactable = _bInteractable;
            }
        }
    }
}
