using UnityEngine;

namespace Dialogue_R
{
    public class Dialogue_Control_R : MonoBehaviour
    {
        public void InputSpeech(ref bool _bWait)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _bWait = false;
                return;
            }
        }
    }
}