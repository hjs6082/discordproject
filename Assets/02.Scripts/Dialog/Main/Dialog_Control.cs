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
    public class Dialog_Control : MonoBehaviour
    {
        private Dialog_Talk dialog_Talk = null;

        private void Awake()
        {
            dialog_Talk = GetComponent<Dialog_Talk>();
        }

        private void Update()
        {

        }

        public void InputSpeech(bool _bWin)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
            }
        }
    }
}
