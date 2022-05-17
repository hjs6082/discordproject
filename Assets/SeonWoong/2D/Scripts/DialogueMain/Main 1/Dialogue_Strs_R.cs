using System.Collections;
using System.Collections.Generic;

namespace Dialogue_R
{
    public class Dialogue_Strs
    {
        public static List<string> GetStrsToList(string[] _strs)
        {
            List<string> _str_List = new List<string>(_strs);

            return _str_List;
        }

        public static readonly string[] EPILOGUE_STRS = {
            "나 왔어-m",
            "회사는 잘 갔다 왔어?-w",
            "응, 잘 갔다 왔지-m",
            "아 근데 전에 예약구매한 게임이 오늘 나와서-m",
            "뭐?-w",
            "나 이거 진짜 기다리던 게임이라 이거 다운로드만 하고 저녁 먹을게-m",
            "혹시 오늘이 무슨 날인지 알고 있어?-w",
            "오늘? 금요일이지-m",
            "이것만 깔아두고 밥 먹을게-m",
            "조금만 기다려줘-m",
            "됐다 그래......-w",
            "너 알아서 해-w",
            "나 먼저 먹고 있을테니까-w",
            "금방 밥 먹으러 갈게!-m",
        };

        public static readonly string[] PC_VIEW_STRS = {
            "오늘 무슨 일 있었나-m",
            "화난 것 같네-m",
            "빨리 게임 다운받고\n무슨 일인지 들어줘야겠다-m",
            "?-m",
            "뭔가 이상한건 기분 탓인가?-m",
            "방금 무슨 소리야?-w",
            "이젠 하다하다 물건도 부수는 거야?-w",
            "....-m",
            "여보?-w",
            "....-m",
            "게임 깐다면서 어딜 간거야-w",
            "이게 뭐지?-w"
        };
    }
}
