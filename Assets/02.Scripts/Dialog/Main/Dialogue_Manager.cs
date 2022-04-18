using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue
{
    public class Dialogue_Manager : MonoBehaviour
    {
        private static Dialogue_Manager instance;
        public static Dialogue_Manager Instance {
            get
            {
                return instance;
            }
        }
        public static Action<bool> damaged;

        private Dialogue_Control dialogue_Ctrl = null;
        private Dialogue_Talk dialogue_Talk = null;


        public Image background = null;
        public Sprite inGameImg = null;
        public Image wife_Image = null;

        public Button action_Button = null;
        public Transform button_Parent = null;
        private List<Button> action_List = new List<Button>();

        [SerializeField] private RectTransform wife_View;
        [SerializeField] private RectTransform pc_View;
        public bool bWifeView = false;

        public bool isTalking = false;
        public bool bWin = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            damaged += Damaged;

            InitClass();
        }

        private void Start()
        {
            if (GameManager.Instance != null)
            {
                dialogue_Talk?.SetName(GameManager.Instance.ManName, GameManager.Instance.WomanName);
            }
            else
            {
                dialogue_Talk?.SetName("남편", "아내");
            }

            InitButtons();

            wife_View.anchoredPosition = new Vector3(0, 0, 0);
            pc_View.anchoredPosition = new Vector3(1920, 0, 0);

            OnOffButtons(false);
        }

        private void Update()
        {

        }

        public void InitClass()
        {
            Transform _parent = this.transform.parent;
            
            dialogue_Ctrl = _parent.GetComponentInChildren<Dialogue_Control>();
            dialogue_Talk = _parent.GetComponentInChildren<Dialogue_Talk>();
        }

        public void Damaged(bool _bWin)
        {
            bWin = _bWin;
            dialogue_Talk.ClearSpeech(dialogue_Talk.GetSpeechText());
            //panelOnOff.OnOff(bWin, null);
        }

        private void InitButtons()
        {
            for (int i = 0; i < 4; i++)
            {
                int index = i;
                Button button = Instantiate(action_Button, button_Parent);
                button.GetComponentInChildren<Text>().text = $"{i + 1}. 공격 {i}";
                button.onClick.AddListener(() =>
                {
                    //panelOnOff.MiniGame(index);
                    OnOffButtons(false);

                    return;
                });
                action_List.Add(button);
            }
        }

        public void OnOffButtons(bool _bInteract)
        {
            for (int i = 0; i < action_List.Count; i++)
            {
                action_List[i].interactable = _bInteract;
            }
        }

        public void ChangeView()
        {
            bWifeView = !bWifeView;
            float offset = (bWifeView) ? 1920.0f : -1920.0f;

            wife_View.DOComplete();
            pc_View.DOComplete();

            wife_View.DOAnchorPosX(wife_View.anchoredPosition.x + offset, 0.5f);
            pc_View.DOAnchorPosX(pc_View.anchoredPosition.x + offset, 0.5f);
        }
    }
}
