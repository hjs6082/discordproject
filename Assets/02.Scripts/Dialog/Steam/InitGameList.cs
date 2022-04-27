using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitGameList : MonoBehaviour
{
    private const string GAME_LIST_PATH = "Steam/GameList/";

    public  GameObject game_Prefab = null;
    private List<GameScriptableObject> game_SO_List = new List<GameScriptableObject>(); 
    private int SO_Amount = 0;

    private void Awake()
    {
        SO_Amount = Resources.LoadAll<GameScriptableObject>(GAME_LIST_PATH).Length;

        for(int i = 0; i < SO_Amount; i++)
        {
            GameScriptableObject game_SO = Resources.Load<GameScriptableObject>(GAME_LIST_PATH + "Game_" + i);

            game_SO_List.Add(game_SO);
        }
    }

    private void Start()
    {
        InitGames();
    }

    private void InitGames()
    {
        for(int i = 0; i < SO_Amount; i++)
        {
            GameObject game = Instantiate<GameObject>(game_Prefab, this.transform);

            Text game_Title = game.GetComponentInChildren<Text>();
            Image game_Icon = game.GetComponentInChildren<Image>();

            game_Title.text  = game_SO_List[i].game_Title;
            game_Icon.sprite = game_SO_List[i].game_Icon;
        }
    }
}
