using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memo
{
    public class PasswordMemo : MonoBehaviour
    {
        public GameObject memoPanel;
        public MemoControll memoCtrl;

        public int passwordIndex; // 몇 번째 자리 비밀번호인지

        private void Awake()
        {
            memoCtrl = memoPanel.GetComponentInChildren<MemoControll>();
        }

        private void OnMouseDown()
        {
            string str = MemoStrs.PasswordMemo[passwordIndex];
            memoPanel.SetActive(true);
            memoCtrl.SetMemoText(str);
        }
    }
}