using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using DG.Tweening.Core;

public class LoadScene : MonoBehaviour
{
    public static void LoadingScene(string sceneName)
    {
        DOTween.KillAll();
        DOTween.Clear();
    
        LoadingSceneManager.LoadScene(sceneName);
    }

    public static void LoadingScene_MainToDialogue()
    {
        DOTween.KillAll();
        DOTween.Clear();
     
        SceneManager.LoadScene("ReNew_Dialogue");
    }
}
