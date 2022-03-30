using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CheatWindow : EditorWindow
{
    [MenuItem("Window/Cheat Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CheatWindow));
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Speed Controller", EditorStyles.boldLabel);

        if(GUILayout.Button("X1.0", GUILayout.Width(100.0f), GUILayout.Height(30.0f)))
        {
            Time.timeScale = 1.0f;
        }

        if(GUILayout.Button("X1.5", GUILayout.Width(100.0f), GUILayout.Height(30.0f)))
        {
            Time.timeScale = 1.5f;
        }

        if(GUILayout.Button("X2.0", GUILayout.Width(100.0f), GUILayout.Height(30.0f)))
        {   
            Time.timeScale = 2.0f;
        }
    }
}
