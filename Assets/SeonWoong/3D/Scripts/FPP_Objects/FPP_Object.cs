using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wife_Scene
{
    public abstract class FPP_Object : MonoBehaviour
    {
        protected abstract List<string> object_Strs_List { get; set; }
        protected abstract FPP_Outline  fpp_Outline      { get; set; }

        protected abstract int  talkCount { get; set; }
        protected abstract bool bWait     { get; set; }

        protected abstract void Awake();
        protected abstract void Start();
        protected abstract void Update();

        protected abstract void OnMouseDown();
        protected abstract void OnMouseEnter();
        protected abstract void OnMouseExit();

        protected abstract void InitValue();
        protected abstract void Talk();

        protected abstract bool CanTouch();

        public virtual IEnumerator NextTalk(Action _onComplete)
        {
            while (bWait)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    bWait = false;

                    Time.timeScale = 1.0f;
                }

                yield return null;
            }

            _onComplete?.Invoke();
        }

        public virtual IEnumerator SkipTalk()
        {
            while (true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Time.timeScale = 5.0f;

                    break;
                }

                yield return null;
            }
        }
    }
}
