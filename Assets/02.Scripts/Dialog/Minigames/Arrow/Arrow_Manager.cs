using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Dialogue
{
    public class Arrow_Manager : Dialogue.Minigame
    {
        private const float DEFAULT_TIME = 4.25f;

        public Arrow_Control  arrow_Ctrl { get; private set; }
        public Arrow_Obj      arrow_Obj  { get; private set; }
        public SpriteRenderer background = null;

        private bool bWin = false;

        protected override void Awake()
        {
            arrow_Ctrl = GetComponentInChildren<Arrow_Control>();
        }

        protected override void Start()
        {
            arrow_Obj = arrow_Ctrl.arrow_Obj;
        }

        protected override void Update()
        {
            if (Dialogue_Manager.Instance.cur_eMinigame == eMinigame.ARROW)
            {
                InputArrow();
            }
        }

        public override void InitGame()
        {
            arrow_Obj?.InitArrow();
        }

        public override void SetGamePos()
        {
            background.transform.parent.position = Dialogue_Manager.DEFAULT_MINIGAME_POSITION;
        }

        public override void LoadGame(Transform _minigameTrm, Action _action = null)
        {
            _minigameTrm = background.transform.parent;

            base.LoadGame(_minigameTrm, () => InitGame());
        }

        public override void Download() // 이겼을 때 다운로드 게이지 채워주는 함수
        {
            background.enabled = false;

            base.Download();
        }

        public void InputArrow()
        {
            bWin = arrow_Ctrl.InputArrow();

            if (bWin)
            {
                Debug.Log("dmddo");
                
                Dialogue_Manager.Instance.MinigameClear();
            }
        }
    }
}