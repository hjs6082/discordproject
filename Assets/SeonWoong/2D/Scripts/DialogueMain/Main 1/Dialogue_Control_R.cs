using UnityEngine;

namespace Dialogue_R
{
    public class Dialogue_Control_R : MonoBehaviour
    {
        public void InputSpeech(ref bool _bWait)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _bWait = false;

                Time.timeScale = 1.0f;
                
                return;
            }
        }
    }
}
