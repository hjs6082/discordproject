using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Util : MonoBehaviour
{
    public static Color SetColor(float _r, float _g, float _b, float _a)
    {
        Color setColor = new Color(_r / 255.0f, _g / 255.0f, _b / 255.0f, _a / 255.0f);

        return setColor;
    }

    public static Color SetColor(int _r, int _g, int _b, int _a)
    {
        Color setColor = new Color(_r, _g, _b, _a);

        return setColor;
    }
}
