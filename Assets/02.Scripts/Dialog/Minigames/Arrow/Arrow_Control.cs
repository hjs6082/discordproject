using System.Collections.Generic;
using UnityEngine;

public class Arrow_Control : MonoBehaviour
{
    public Arrow_Obj arrow_Obj { get; private set; }

    private Dictionary<KeyCode, int> arrow_Keys = new Dictionary<KeyCode, int>()
    {
        {KeyCode.UpArrow, 0},
        {KeyCode.DownArrow, 1},
        {KeyCode.LeftArrow, 2},
        {KeyCode.RightArrow, 3}
    };

    private void Awake()
    {
        arrow_Obj = GetComponentInChildren<Arrow_Obj>();
    }

    public bool InputArrow()
    {
        bool bCheck = false;

        if (Input.anyKeyDown)
        {
            foreach (var input in arrow_Keys)
            {
                if (Input.GetKeyDown(input.Key))
                {
                    bCheck = arrow_Obj.bCheckArrow(input.Value);
                }
            }
        }

        return bCheck;
    }
}
