using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace Wife_Scene
{
    public class Shelf_Abstract : FPP_Object
    {
        private const float CLOSE_POS_Z = 0.58f;
        private const float OPEN_POS_Z  = 1.00f;

        protected override List<string> object_Strs_List { get; set; } = null;
        protected override FPP_Outline  fpp_Outline      { get; set; } = null;

        protected override int  talkCount { get; set; } = 0;
        protected override bool bWait     { get; set; } = false;

        public  bool isLocked = false;
        private bool isOpen   = false;
  
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
            object_Strs_List = new List<string>(FPP_Strs.GetStringArrToList(FPP_Strs.SHELF_STRS));
            fpp_Outline = GetComponent<FPP_Outline>();

            talkCount = 0;
            bWait = false;

            isOpen = false;
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
                    }));
                });
            }
            else
            {
                FPP_Manager.Instance.FindObjectTalk(object_Strs_List[talkCount], () =>
                {
                    StartCoroutine(NextTalk(() =>
                    {
                        FPP_Manager.Instance.GetMove().bObject = false;
                        FPP_Manager.Instance.OnOffText(false);

                        talkCount = 0;
                    }));
                });
            }
        }

        protected override bool CanTouch()
        {
            bool _canTouch = false;
            float _distance = Vector3.Distance(transform.position, FPP_Manager.Instance.GetMove().player.position);

            _canTouch = _distance <= 1.0f && !FPP_Manager.Instance.GetMove().bObject;

            return _canTouch;
        }

        protected override void OnMouseDown()
        {
            if (CanTouch())
            {
                for(int i = 0; i < Inventory.instance.items.Count; i++)
                {
                    if(Inventory.instance.items[i].itemName == "서랍 키")
                    {
                        isLocked = false;
                        break;
                    }
                }

                if(isLocked)
                {
                    FPP_Manager.Instance.OnOffText(true);

                    Talk();
                }
                else
                {
                    float moveOffset = (isOpen) ? CLOSE_POS_Z : OPEN_POS_Z;
                    MoveShelf(moveOffset);
                }
            }
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

        private void MoveShelf(float _offset)
        {
            isOpen = !isOpen;
            FPP_Manager.Instance.GetMove().bObject = true;
            transform.DOLocalMoveZ(_offset, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => { FPP_Manager.Instance.GetMove().bObject = false; });
        }
    }
}
