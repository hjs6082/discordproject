using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Memo
{
    public class ChessMemo : MonoBehaviour
    {
        private Vector3 startPos = new Vector3(2.8f, 0.03f, 2.7f);
        private Vector3 startRotate = new Vector3(0f, -44f, 0f);

        private Vector3 nextPos = new Vector3(11f, 0f, 6f);
        private Vector3 nextRotate = Vector3.zero;

        public GameObject memoPanel;
        private MemoControll memoCtrl;

        private void Awake()
        {
            memoPanel.SetActive(false);
            memoCtrl = memoPanel.GetComponentInChildren<MemoControll>();
            //SetMemoTrm(startPos, startRotate);
        }

        private void SetMemoTrm(Vector3 pos, Vector3 rotate)
        {
            this.gameObject.transform.position = pos;
            this.gameObject.transform.rotation = Quaternion.Euler(rotate);
        }

        private void OnMouseDown()
        {
            if (GameManager.Instance.isPuzzle == true)
            {
                string str = "";

                for (int i = 0; i < MemoStrs.ChessStoryMemo.Length; i++)
                {
                    str += MemoStrs.ChessStoryMemo[i];
                }

                memoPanel.SetActive(true);
                memoCtrl.SetMemoText(str);
                SetMemoTrm(nextPos, nextRotate);
            }
        }
    }
}