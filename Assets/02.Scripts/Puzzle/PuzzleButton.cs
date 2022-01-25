using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButton : MonoBehaviour
{
    public GameObject map;

    [SerializeField]
    MovePuzzle mp;

    public bool show = false;
    public void Show()
    {
        if (show == false)
        {
            map.SetActive(true);
            show = true;
        }
        else
        {
            map.SetActive(false);
            show = false;
        }
    }

    public void ResetButton()
    {
        mp.Shuffle();
    }
}
