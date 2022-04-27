using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialogue
{
    public class BingBang_Manager : Minigame
    {
        private const int DEFAULT_LIFE = 5;

        private BingBang_Control bingBang_Ctrl = null;
        private BingBang_Obj bingBang_Obj = null;
        //[SerializeField] private GameObject readyPanel = null;

        private int currentLife = 0;

        private bool bStart = false;

        protected override void Awake()
        {
            bingBang_Ctrl = GetComponentInChildren<BingBang_Control>();

            bingBang_Obj = bingBang_Ctrl.bingBang_Obj;
        }

        protected override void Start()
        {
            InitGame();
        }

        protected override void Update()
        {
            if (bStart)
            {
                bool bSuccess = bingBang_Ctrl.TwinkleTurn();

                if (!bSuccess) LifeDown();
            }
            else
            {
                //StartGame(readyPanel);
            }
        }

        public override void InitGame()
        {
            InitValue();
            bingBang_Obj.RandomTwinkle();
        }

        private void InitValue()
        {
            currentLife = DEFAULT_LIFE;
        }

        private void LifeDown()
        {
            currentLife--;
        }

        //public override void StartGame(GameObject _readyPanel)
        // {
        //     base.StartGame(_readyPanel);
        // }

        public override void SetGamePos()
        {
            
        }

        public override void LoadGame(Transform _minigameTrm, Action _action = null)
        {
            base.LoadGame(_minigameTrm);
        }

        public override void Download()
        {
            Dialogue_Manager.Instance.ChangeView(() => 
            {
                float curAmount = Dialogue_Manager.Instance.download_Guage.fillAmount;
                Dialogue_Manager.Instance.download_Guage.DOFillAmount(curAmount + 0.1f, 0.5f)
                .SetDelay(0.5f)
                .OnComplete(() => 
                {
                    Dialogue_Manager.Instance.ChangeView();
                });
            });
        }
    }
}
