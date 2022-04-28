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
        
    }
}
