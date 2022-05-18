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

        public static readonly string[] GAME_INFO_STRS = {
            "어머나 큰일 났어요!\n동화세계가 위험에 빠져버리고 말았어요. 모든 동화가 합쳐졌네요.\n정해진 구역을 돌아다니며 각 동화의 아름다운 모습이 담겨있는 사진을 찾으면 동화세계를\n구할 수 있어요! 도와주실 거죠?",
            "어머나 큰일 났어요!\n동화세계가 위험에 빠져버리고 말았어요. 모든 동화가 합쳐졌네요!\n동화뿐만 아니라 다른 것도 섞인 것 같은데...\n정해진 구역을 돌아다니며 그때의 아름다운 모습이 담겨있는 사진을 찾으면 동화세계를\n구할 수 있어요. 어쩌면 동화세계 말고 다른 것도 구할 수 있겠죠?\n저희를 꼭 도와주셔야 할 거예요!"
        };
    }
}
