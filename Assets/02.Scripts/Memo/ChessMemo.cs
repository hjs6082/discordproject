using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memo
{
    public class ChessMemo : MonoBehaviour
    {
        public GameObject memoPanel;
        private MemoControll memoCtrl;

        private void Awake()
        {
            memoCtrl = memoPanel.GetComponentInChildren<MemoControll>();
        }

        private void OnMouseDown()
        {
            string str = "";
            
            for (int i = 0; i < MemoStrs.ChessStoryMemo.Length; i++)
            {
                str += MemoStrs.ChessStoryMemo[i];
            }

            memoPanel.SetActive(true);
            memoCtrl.SetMemoText(str);
        }
    }
}