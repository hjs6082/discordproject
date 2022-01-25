using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public string ManName = "";
    public string WomanName = "";

    public AudioClip[] BGM_Arr;
    private AudioSource bgmAudio;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeBGM(int index)
    {
        bgmAudio.Stop();
        bgmAudio.clip = BGM_Arr[index];
        bgmAudio.Play();
    }
}