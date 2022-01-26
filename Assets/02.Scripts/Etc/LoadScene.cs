using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadScene : MonoBehaviour
{
    public static void LoadingScene(string sceneName)
    {
        DOTween.Clear(true);
        if(GameManager.Instance.DemoClearCheck())
        {
            GameManager.Instance.ClearPanel.SetActive(true);
        }
        SceneManager.LoadScene(sceneName);
    }
}
