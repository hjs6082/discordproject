using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public static void LoadingScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
