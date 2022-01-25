using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Memo
{
    public class MemoControll : MonoBehaviour
    {
        public Button backButton;
        private Text memoText;

        private void Awake()
        {
            memoText = GetComponentInChildren<Text>();
            backButton.onClick.AddListener(() => { MemoOff(); });
        }

        private void MemoOff()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        public void SetMemoText(string memoStr)
        {
            memoText.text = memoStr;
        }
    }
}