using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Dialogue
{
    public abstract class Minigame : MonoBehaviour
    {
        protected abstract void Awake();
        protected abstract void Start();
        protected abstract void Update();

        public    abstract void InitGame();
        public    abstract void SetGamePos();

        public virtual void LoadGame(Transform _minigameTrm, Action _action = null)
        {
            Dialogue_Manager.Instance.isDoingGame = true;

            _minigameTrm.DOMoveY(0.0f, 0.5f)
            .OnComplete(() => 
            {
                _action?.Invoke();
            });
        }
        public virtual void Download()
        {
            Dialogue_Manager.Instance.ChangeView(() => 
            {
                float curAmount = Dialogue_Manager.Instance.download_Guage.fillAmount;
                Dialogue_Manager.Instance.download_Guage.DOFillAmount(curAmount + 0.1f, 0.5f)
                .SetDelay(0.5f)
                .OnComplete(() => 
                {
                    Dialogue_Manager.Instance.ChangeView(() => 
                    {
                        ref int index = ref Dialogue_Manager.Instance.mainTalk_Index;
                        Dialogue_Manager.Instance.dlg_Talk.Main_Talk(index);
                        index++;
                    });
                });
            });
        }
    }
}
