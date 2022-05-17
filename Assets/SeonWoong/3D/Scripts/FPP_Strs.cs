using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPP_Strs
{
    public static List<string> GetStringArrToList(string[] _strs_ToGet)
    {
        List<string> _strs_List = new List<string>(_strs_ToGet);

        return _strs_List;
    }

    public static readonly string[] FPP_MANAGER_STRS =
    {
        "",
        ""
    };

    public static readonly string[] ALBUM_STRS = 
    {
        "",
        "",
        "",
        ""
    };

    public static readonly string[] BOOK_STRS = 
    {
        "...",
        "이건 앨범이 아닌 듯 하다"
    };

    public static readonly string[] HOUSE_KEY_STRS = 
    {
        "",
        ""
    };

    public static readonly string[] PHOTO_STRS = 
    {
        "",
        ""
    };
}
