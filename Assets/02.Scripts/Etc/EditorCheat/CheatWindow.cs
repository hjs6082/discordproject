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
        GUILayout.Label("Skip Dialogue", EditorStyles.boldLabel) ;

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();


        bool isOldWideMode = EditorGUIUtility.wideMode;
        EditorGUIUtility.wideMode = true;

        if (GUILayout.Button("Skip", GUILayout.Width(100.0f), GUILayout.Height(40.0f)))
        {
            Debug.Log("스킵");
            if(Application.isPlaying)
            {
                Dialogue.Dialogue_Manager.Instance.SkipDialog();
            }

        }
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
}
