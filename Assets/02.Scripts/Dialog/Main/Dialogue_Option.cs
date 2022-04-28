using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogue
{
    public class Dialogue_Option : MonoBehaviour
    {
        private Dictionary<KeyCode, int> option_Key_Dic = new Dictionary<KeyCode, int>()
        {
            {KeyCode.W,         -1},
            {KeyCode.UpArrow,   -1},
            {KeyCode.S,         +1},
            {KeyCode.DownArrow, +1}
        };
    
        public List<Button> option_List        = new List<Button>();
        public Transform    option_Element_Trm = null;
        public Button       curOption          = null;

        public int optionIndex = 0;

        public bool canSelect = false;

        private void Update()
        {
            InputOption();
        }

        public void SetCurOption(int _index, bool bSwitchColor = true)
        {
            optionIndex += _index;

            if (option_List.Count != 0)
            {
                if (option_List[optionIndex % option_List.Count] != null)
                {
                    curOption = option_List[optionIndex % option_List.Count];

                    if (bSwitchColor)
                    {
                        InitOptionColor();
                    }
                }
            }
        }

        public void InitOptionColor()
        {
            for (int i = 0; i < option_List.Count; i++)
            {
                option_List[i].GetComponentInChildren<Text>().color = Color.black;
            }

            curOption.GetComponentInChildren<Text>().color = Color.red;
        }

        public void InitOption()
        {
            Button[] option_Arr = option_Element_Trm.GetComponentsInChildren<Button>();

            for (int i = 0; i < option_Arr.Length; i++)
            {
                option_List.Add(option_Arr[i]);
            }

            optionIndex = 0;
        }

        private void InputOption()
        {
            if (canSelect && !Dialogue_Manager.Instance.isDoingGame)
            {
                if (Input.anyKeyDown)
                {
                    foreach (var item in option_Key_Dic)
                    {
                        if (option_List.Count > 0)
                        {
                            if (Input.GetKeyDown(item.Key) && option_List.Count > 0 && !Dialogue_Manager.Instance.isTalking)
                            {
                                SetCurOption(item.Value);
                                return;
                            }
                            else if (Input.GetKeyDown(KeyCode.Space) && !Dialogue_Manager.Instance.isMoving)
                            {
                                curOption.onClick.Invoke();
                                curOption.GetComponentInChildren<Text>().color = Color.blue;
                                curOption.onClick.RemoveAllListeners();
                                option_List.Remove(curOption);
                                optionIndex++;
                                SetCurOption(1, false);
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
