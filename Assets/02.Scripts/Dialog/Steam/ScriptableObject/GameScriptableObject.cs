using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Scriptable Object/GameScriptableObject", order = 1)]
public class GameScriptableObject : ScriptableObject
{
    public string game_Title = string.Empty;

    public Sprite game_Icon = null;
}
