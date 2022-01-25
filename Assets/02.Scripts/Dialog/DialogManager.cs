using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        private DialogControll dialogCtrl;
        private string KeyName = "QWER";

        public GameObject dialogButtonPrefab;
        public Transform btnParent;

        public Text ManNameText;
        public Text WomanNameText;

        private void Awake()
        {
            if(GameManager.Instance != null)
            {
                if(ManNameText != null) { ManNameText.text = GameManager.Instance.ManName; }
                if(WomanNameText != null) { WomanNameText.text = GameManager.Instance.WomanName; }
            }

            dialogCtrl = GetComponent<DialogControll>();

            for(int i = 0; i < 4; i++)
            {
                int order = i;
                InitButton(dialogButtonPrefab, order, KeyName[i]);
            }
        }

        private void InitButton(GameObject buttonPrefab, int order, char keyName)
        {
            GameObject button = Instantiate(buttonPrefab, btnParent);

            button.GetComponent<Button>().onClick.AddListener(() => 
            {
                if(!dialogCtrl.isTalking && dialogCtrl.isPlayer)
                {
                    eIndex type = eIndex.MAN;
                    dialogCtrl.Next(type, order);
                }
            });
            button.GetComponentInChildren<Text>().text = keyName.ToString();
        }
    }
}
