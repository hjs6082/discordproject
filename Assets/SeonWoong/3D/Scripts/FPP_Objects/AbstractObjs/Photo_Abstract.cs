using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wife_Scene
{
    public class Photo_Abstract : FPP_Object
    {
        protected override List<string> object_Strs_List { get; set; } = null;
        protected override FPP_Outline  fpp_Outline      { get; set; } = null;

        protected override int  talkCount { get; set; } = 0;
        protected override bool bWait     { get; set; } = false;

        [SerializeField] private Sprite photo_Sprite = null;

        private MeshRenderer  itemRenderer = null;
        private MeshCollider  itemCollider = null;

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
            object_Strs_List = new List<string>(FPP_Strs.GetStringArrToList(FPP_Strs.PHOTO_STRS));
            fpp_Outline = GetComponent<FPP_Outline>();

            talkCount = 0;
            bWait = false;

            itemRenderer = GetComponent<MeshRenderer>();
            itemCollider = GetComponent<MeshCollider>();
        }

        protected override void Talk()
        {
            FPP_Manager.Instance.GetMove().bObject = true;
            bWait = true;

            StartCoroutine(SkipTalk());

            if (talkCount < object_Strs_List.Count - 1)
            {
                FPP_Manager.Instance.FindObjectTalk(object_Strs_List[talkCount], () =>
                {
                    StartCoroutine(NextTalk(() =>
                    {
                        talkCount++;
                        Talk();

                        if(talkCount == 2)
                        {
                            GameManager.Instance.AddPhoto(photo_Sprite);
                            GameManager.Instance.BookCtrl(ePage.ALBUM);
                        }
                    }));
                });
            }
            else      
            {
                FPP_Manager.Instance.FindObjectTalk(object_Strs_List[talkCount], () =>
                {
                    StartCoroutine(NextTalk(() =>
                    {
                        talkCount = 0;

                        GameManager.Instance.BookCtrl(ePage.ALBUM);

                        FPP_Manager.Instance.GetMove().bObject = false;
                        FPP_Manager.Instance.OnOffText(false);

                        FPP_MouseCursor.ChangeCursor(eCursor.NORMAL);

                        Destroy(this.gameObject);
                    }));
                });
            }
        }

        protected override bool CanTouch()
        {
            bool _canTouch = false;
            float _distance = Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position);

            _canTouch = _distance <= 2.0f && !FPP_Manager.Instance.GetMove().bObject;

            return _canTouch;
        }

        protected override void OnMouseDown()
        {
            if (CanTouch())
            {
                FPP_Manager.Instance.OnOffText(true);

                itemRenderer.enabled = false;
                itemCollider.enabled = false;

                Talk();
            }
        }

        protected override void OnMouseEnter()
        {
            if (CanTouch())
            {
                fpp_Outline.OnOutline();

                FPP_MouseCursor.ChangeCursor(eCursor.HAND);
            }
        }

        protected override void OnMouseExit()
        {
            if (CanTouch())
            {
                fpp_Outline.OffOutline();

                FPP_MouseCursor.ChangeCursor(eCursor.NORMAL);
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