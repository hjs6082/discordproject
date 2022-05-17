using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_MouseCursor : MonoBehaviour
{
    private static Vector2 cursor_HotSpot = new Vector2(0.0f, 0.0f);

    public static void ChangeCursor(Texture2D _cursor, bool bHotspot = true)
    {
        cursor_HotSpot.x = (bHotspot) ? _cursor.width * 0.5f : 0.0f;
        cursor_HotSpot.y = (bHotspot) ? _cursor.height * 0.5f : 0.0f;

        Cursor.SetCursor(_cursor, cursor_HotSpot, CursorMode.ForceSoftware);
    }
}
