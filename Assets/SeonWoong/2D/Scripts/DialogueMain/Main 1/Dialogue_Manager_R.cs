using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue_R
{
    public class Dialogue_Manager_R : MonoBehaviour
    {
        #region 싱글톤
        private static Dialogue_Manager_R instance;
        public static Dialogue_Manager_R Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public static readonly Dialogue_Strs dlg_Strs = new Dialogue_Strs();
        public static Action download = null;

        public Dialogue_Control_R dlg_Ctrl   { get; private set; }
        public Dialogue_Talk_R    dlg_Talk   { get; private set; }
        public Dialogue_Option_R  dlg_Option { get; private set; }

        // 이름
        private string manName   = "남편";
        private string womanName = "아내";

        public Image          background = null;
        public Sprite         inGameImg  = null;
        public Image          wife_Image = null;
        public SpriteRenderer fade_SR    = null;

        [SerializeField] private RectTransform wife_View = null;
        [SerializeField] private RectTransform pc_View   = null;

        public Image download_Guage = null;

        public int sub_Index      = 0;
        public int mainTalk_Index = 0;
  
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

            GameManager.Instance?.FadePanel.SetActive(true);
            GameManager.Instance?.Fade_In(1.5f);
        }

        private void Start()
        {
            InitViewPos();
            InitNames();

            GameManager.Instance.Fade_In();
        }

        private void Update()
        {
            
        }

        public void SkipDialog()
        {
            LoadScene.LoadingScene("TestMap");
        }

        public void InitClass() 
        {
            Transform _parent = this.transform.parent;

            dlg_Ctrl   = _parent.GetComponentInChildren<Dialogue_Control_R>();
            dlg_Talk   = _parent.GetComponentInChildren<Dialogue_Talk_R>();
            dlg_Option = _parent.GetComponentInChildren<Dialogue_Option_R>();

            fade_SR = GetComponent<SpriteRenderer>();
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

            if(manName == string.Empty) manName = "남편";
            if(womanName == string.Empty) womanName = "아내";

            dlg_Talk?.SetName(manName, womanName);
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
