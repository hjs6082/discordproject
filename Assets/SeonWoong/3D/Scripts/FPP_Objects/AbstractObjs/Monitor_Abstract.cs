using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace Wife_Scene
{
    public class Monitor_Abstract : FPP_Object
    {
        protected override List<string> object_Strs_List { get; set; } = null;
        protected override FPP_Outline  fpp_Outline      { get; set; } = null;

        protected override int  talkCount { get; set; } = 0;
        protected override bool bWait     { get; set; } = false;

        private Book_Main book_Main = null;

        protected override void Awake()
        {
            InitValue();
        }

        protected override void Start()
        {
            
        }

        protected override void Update()
        {
            
        }
       
        protected override void InitValue()
        {
            fpp_Outline = GetComponent<FPP_Outline>();

            talkCount = 0;
            bWait = false;

            book_Main = GameManager.Instance.book.GetComponent<Book_Main>();
        }

        protected override void Talk()
        {
            
        }

        protected override bool CanTouch()
        {
            bool _canTouch = false;
            float _distance = Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position);

            _canTouch = _distance <= 2.0f && !FPP_Manager.Instance.GetMove().bObject && GameManager.Instance.album_Images[0] != null;

            return _canTouch;
        }

        protected override void OnMouseDown()
        {
            for(int i = 0; i < GameManager.Instance.GetBook_Main().checkList_List.Count; i++)
                {
                    GameObject checkList = GameManager.Instance.GetBook_Main().checkList_List[i];
                    if(checkList.GetComponent<TextMeshProUGUI>().text == FPP_Manager.FPP_CHECKLIST_STRS[0])
                    {
                        checkList.GetComponentInChildren<Toggle>().isOn = true;
                    }
                }

            FPP_Manager.Instance.EndMove(() =>
            {
                GameManager.Instance.Fade_Out(0.5f, () =>
                {
                    LoadScene.LoadingScene("MapJunseo");
                }, Ease.Linear, Color.white);
            });
        }

        protected override void OnMouseEnter()
        {
            if (CanTouch())
            {
                fpp_Outline.OnOutline();

                
            }
        }

        protected override void OnMouseExit()
        {
            if (CanTouch())
            {
                fpp_Outline.OffOutline();
            }
        }

        public override IEnumerator NextTalk(Action _onComplete)
        {
            return base.NextTalk(_onComplete);
        }

        public override IEnumerator SkipTalk()
        {
            return base.SkipTalk();
        }
    }
}
