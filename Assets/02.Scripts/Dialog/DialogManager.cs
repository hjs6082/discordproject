using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialog
{
    public enum eIndex
    {
        MAN,
        WOMAN
    }

    public class DialogManager : MonoBehaviour
    {
        public static DialogManager Instance { get; private set; }
        public static Action<bool> damaged;

        public Color SetColor(float r, float g, float b, float a)
        {
            Color color = new Color(r / 255f, g / 255f, b / 255f, a / 255f);

            return color;
        }

        public ManDialogControl manCtrl;
        public WomanDialogControl womanCtrl;

        private List<DialogControl_main> ctrlList = new List<DialogControl_main>();

        public CharacterVoice charVoice;

        public Text ManName_Text;
        public Text WomanName_Text;

        public Image background;
        public Sprite inGameImg;

        public bool isTalking = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            damaged += Damaged;
        }

        private void Start()
        {

        }

        private void Update()
        {
                     
        }

        public void Damaged(bool _bWin)
        {

        }
    }
}
