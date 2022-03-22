using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public enum eIndex
    {
        MAN,
        WOMAN
    }

    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance { get; private set; }
        public static Action<bool> damaged;

        public Color SetColor(float r, float g, float b, float a)
        {
            Color color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);

            return color;
        }

        public ManDialogControl manCtrl;
        public WomanDialogControl womanCtrl;

        private List<DialogControl_main> ctrlList = new List<DialogControl_main>();

        public CharacterVoice charVoice;

        public Text ManName_Text;
        public Text WomanName_Text;

        public Image background;
        public Sprite inGameImg;

        public PanelOnOff panelOnOff = null;
        public Button action_Button = null;
        public Transform button_Parent = null;
        private List<Button> action_List = new List<Button>();

        public GameObject[] hearts;
        public Transform[] heart_Parents;
        private List<GameObject> blue_Heart_List = new List<GameObject>();
        private List<GameObject> red_Heart_List = new List<GameObject>();

        public bool isTalking = false;

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

            damaged += Damaged;
        }

        private void Start()
        {
            InitButtons();
        }

        private void Update()
        {

        }

        public void Damaged(bool _bWin)
        {
            int heart_Index = (_bWin) ? 0 : 1;

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

            OnOffButtons();

            panelOnOff.OnOff(null);
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
                });
                action_List.Add(button);
            }
        }

        public void OnOffButtons()
        {
            for(int i = 0; i < action_List.Count; i++)
            {
                action_List[i].interactable = (action_List[i].interactable) ? false : true;
            }
        }
    }
}
