using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Dialogue
{
    public class Dialogue_Control : MonoBehaviour
    {
        public void InputSpeech(ref bool _bWait)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _bWait = false;
            }
        }
    }
}
