using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Dialog
{
    public class DialogControll : MonoBehaviour
    {
        private Color SetColor(float r, float g, float b, float a)
        {
            return new Color(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }

        private void Awake()
        {
            
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            
        }

        public void Next(eIndex type, int talkVal)
        {
            
        }

        public void Talking(GameObject dialog, string str) // 대화
        {
            
        }

        public void Damaged(RectTransform obj)
        {
            
        }

        public void SkipSpeech(eIndex type)
        {
            
        }
    }
}
