using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue
{
    public enum eMinigame
    {
        MAZE,
        ARROW,
        ONEDRAW,
        NONE
    }

    public class Dialogue_Manager : MonoBehaviour
    {
        #region 싱글톤
        private static Dialogue_Manager instance;
        public static Dialogue_Manager Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public static readonly Dialogue_Strs dlg_Strs = new Dialogue_Strs();
        public static readonly Vector3 DEFAULT_MINIGAME_POSITION = new Vector3(0.0f, 10.4f, 0.0f);
        public static Action download = null;

        public Dialogue_Control dlg_Ctrl   { get; private set; }
        public Dialogue_Talk    dlg_Talk   { get; private set; }
        public Dialogue_Option  dlg_Option { get; private set; }

        // 이름
        private string manName   = "남편";
        private string womanName = "아내";

        public Image          background = null;
        public Sprite         inGameImg  = null;
        public Image          wife_Image = null;
        public SpriteRenderer fade_SR    = null;

        public Button        action_Button = null;
        public Transform     button_Parent = null;
        private List<Button> action_List   = new List<Button>();

        [SerializeField] private RectTransform wife_View = null;
        [SerializeField] private RectTransform pc_View   = null;

        public Image          download_Guage        = null;
        public GameObject[]   sub_Background_List   = null;
        public List<Vector3>  old_Background_Pos    = new List<Vector3>();
        public List<Minigame> minigame_List         = new List<Minigame>();
        public eMinigame      cur_eMinigame         = eMinigame.NONE;
        public Text           minigame_Explain_Text = null; 
        public int            sub_Index             = 0;
        public int            mainTalk_Index        = 0;
  
        public bool bWifeView   = false;
        public bool isTalking   = false;
        public bool isMoving    = false;
        public bool isDoingGame = false;
        public bool bWin        = false;

        private void Awake()
        {
            #region 싱글톤
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion

            InitClass();
        }

        private void Start()
        {
            InitButtons();
            InitViewPos();
            InitNames();
            InitMinigamePos();

            for (int i = 0; i < sub_Background_List.Length; i++)
            {
                old_Background_Pos.Add(sub_Background_List[i].transform.position);
            }

            minigame_Explain_Text.transform.parent.gameObject.SetActive(false);
        }

        private void Update()
        {

        }

        public void InitClass() 
        {
            Transform _parent = this.transform.parent;

            dlg_Ctrl   = _parent.GetComponentInChildren<Dialogue_Control>();
            dlg_Talk   = _parent.GetComponentInChildren<Dialogue_Talk>();
            dlg_Option = _parent.GetComponentInChildren<Dialogue_Option>();

            fade_SR = GetComponent<SpriteRenderer>();
        }

        private void InitButtons()
        {
            for (int i = 0; i < minigame_List.Count; i++)
            {
                int index = i;
                Button button = Instantiate(action_Button, button_Parent);

                button.GetComponentInChildren<Text>().text = $"{index + 1}. {dlg_Strs.option_Name[i]}";
                button.interactable = false;
                button.onClick.AddListener(() =>
                {
                    if (dlg_Option.canSelect)
                    {
                        //panelOnOff.MiniGame(index);
                        Debug.Log("공격공격");

                        // TODO : 미니게임 켜기

                        if (!isMoving)
                        {
                            SetMinigame(index);
                        }
                    }

                    return;
                });
                action_List.Add(button);
            }

            dlg_Option.InitOption();
        }

        private void InitViewPos()
        {
            bWifeView = true;

            wife_View.anchoredPosition = new Vector3(   0.0f, 0.0f, 0.0f);
            pc_View.anchoredPosition   = new Vector3(1920.0f, 0.0f, 0.0f);
        }

        private void InitNames()
        {
            bool isInstanceTrue = (GameManager.Instance != null) ? true : false;

            manName   = (isInstanceTrue) ? GameManager.Instance.ManName : manName;
            womanName = (isInstanceTrue) ? GameManager.Instance.WomanName : womanName;

            dlg_Talk?.SetName(manName, womanName);
        }

        private void InitMinigamePos()
        {
            for (int i = 0; i < minigame_List.Count; i++)
            {
                minigame_List[i].GetComponent<Minigame>().SetGamePos();
            }
        }

        public void ChangeView(Action _action = null, float _delay = 0.0f)
        {
            bWifeView = !bWifeView;

            float offset = (bWifeView) ? 1920.0f : -1920.0f;

            wife_View.DOComplete();
            pc_View.DOComplete();

            wife_View.DOAnchorPosX(wife_View.anchoredPosition.x + offset, 0.5f)
            .SetDelay(_delay)
            .SetEase(Ease.OutSine);

            pc_View.DOAnchorPosX(pc_View.anchoredPosition.x + offset, 0.5f)
            .SetDelay(_delay)
            .SetEase(Ease.OutSine)
            .OnComplete(() => 
            {
                _action?.Invoke();
            });
        }

        public void MinigameClear()
        {
            isMoving = true;
            int cur_eMinigame_Num = (int)cur_eMinigame;

            minigame_Explain_Text.transform.parent.gameObject.SetActive(false);

            MoveToPos(sub_Background_List[sub_Index], old_Background_Pos[sub_Index], () =>
            {
                sub_Index--;
                MoveToPos(sub_Background_List[sub_Index], old_Background_Pos[sub_Index], () =>
                {
                    sub_Index--;
                    MoveToPos(sub_Background_List[sub_Index], old_Background_Pos[sub_Index], () =>
                    {
                        sub_Index--;
                        MoveToPos(sub_Background_List[sub_Index], old_Background_Pos[sub_Index], () =>
                        {
                            minigame_List[cur_eMinigame_Num].Download();
                            minigame_List[cur_eMinigame_Num].gameObject.SetActive(false);

                            isMoving = false;
                        });
                    });
                });
            });
        }

        public void SetMinigame(int _minigame)
        {
            sub_Index = 0;
            Vector3 zero = new Vector3(0.0f, 0.0f, 0.0f);

            isMoving = true;
            isDoingGame = true;
            MoveToPos(sub_Background_List[sub_Index], zero, () =>
            {
                sub_Index++;
                MoveToPos(sub_Background_List[sub_Index], zero, () =>
                {
                    sub_Index++;
                    MoveToPos(sub_Background_List[sub_Index], zero, () =>
                    {
                        sub_Index++;
                        MoveToPos(sub_Background_List[sub_Index], zero, () =>
                        {
                            cur_eMinigame = (eMinigame)_minigame;
                            minigame_List[_minigame].LoadGame(minigame_List[_minigame].transform);

                            minigame_Explain_Text.transform.parent.gameObject.SetActive(true);
                            minigame_Explain_Text.text = dlg_Strs.minigame_Explains[(int)cur_eMinigame];

                            isMoving = false;
                        });
                    });
                });
            });
        }

        private void MoveToPos(GameObject _obj, Vector3 _to, Action _action = null)
        {
            _obj.transform.DOMove(_to, 0.25f).OnComplete(() =>
            {
                _action?.Invoke();
            });
        }

        public void FadeDialogue(Action _action = null)
        {
            fade_SR.DOFade(1.0f,  0.75f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => 
            {
                _action?.Invoke();
            });
        }
    }
}
