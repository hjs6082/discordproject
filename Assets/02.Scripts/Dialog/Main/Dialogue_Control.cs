using UnityEngine;

namespace Dialogue
{
    public class Dialogue_Control : MonoBehaviour
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
