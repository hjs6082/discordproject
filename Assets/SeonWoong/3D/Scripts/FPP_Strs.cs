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
        "계속 느낀건데 아무래도\n이 게임 이상하단 말이야",
        "아까 배너에서 부터\n남편이 없어진 것도 그렇고",
        "말도 안되는데 그거 말곤\n지금 생각나는게 없잖아",
        "방이라도 좀 뒤져볼까"
    };

    public static readonly string[] ALBUM_STRS = 
    {
        "...",
        "이거 앨범인데\n내용이 왜 이러지",
        "원래 여기 사진이 있을텐데",
        "전에 이 방에서 봤던 사진이 있던건가",
        "좀 더 찾아봐야겠다"
    };

    public static readonly string[] BOOK_STRS = 
    {
        "...",
        "이건 앨범이 아닌 듯 하다"
    };

    public static readonly string[] SHELF_STRS = 
    {
        "열쇠가 어디 있을텐데"
    };

    public static readonly string[] HOUSE_KEY_STRS = 
    {
        "어, 여기 떨어져있었네",
        "쇼파 위에서 떨어졌던건가"
    };

    public static readonly string[] PHOTO_STRS = 
    {
        "여기 있었네",
        "일단 앨범에 넣어놓고\n게임을 다시 플레이 해봐야겠다"
    };
}
