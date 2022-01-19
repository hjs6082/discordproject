using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public class DialogManager : MonoBehaviour
    {
        private DialogControll dialogControll;

        public GameObject dialogButtonPrefab;
        public Transform btnParent;


        private void Awake()
        {
            dialogControll = GetComponent<DialogControll>();

            for(int i = 0; i < 4; i++)
            {
                int order = i;
                InitButton(dialogButtonPrefab, order);
            }
        }

        private void InitButton(GameObject buttonPrefab, int order)
        {
            GameObject button = Instantiate(buttonPrefab, btnParent);

            button.GetComponent<Button>().onClick.AddListener(() => 
            {
                if(!dialogControll.bTalking)
                dialogControll.Talking(0, order);
            });
        }
    }
}
