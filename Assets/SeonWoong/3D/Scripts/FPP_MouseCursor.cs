using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_MouseCursor : MonoBehaviour
{
    public static void InitCursor()
    {
        FPP_Manager.Instance.cursor_Image.sprite = FPP_Manager.Instance.cursor_Textures[(int)eCursor.NORMAL];
        FPP_Manager.Instance.cursor_Image.rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    public static void ChangeCursor(eCursor _cursor)
    {
        FPP_Manager.Instance.cursor_Image.sprite = FPP_Manager.Instance.cursor_Textures[(int)_cursor];
    }
}
