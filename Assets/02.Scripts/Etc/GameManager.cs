using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    const string fileName = "PuzzleSave";

    public static GameManager Instance { get; private set; }

    public string ManName = "";
    public string WomanName = "";

    public AudioClip[] BGM_Arr;
    private AudioSource bgmAudio;

    public bool bChessClear = false;
    public bool bMovePuzzleClear = false;
    public bool bOnIsland = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }


        SaveData savePuzzle = SaveSystem.Load(fileName);

        if (savePuzzle != null)
        {
            bChessClear = savePuzzle.bChessClear;
            bMovePuzzleClear = savePuzzle.bMovePuzzleClear;
            bOnIsland = savePuzzle.bOnIsland;
        }

        Debug.Log(bChessClear);
    }

    public void ChangeBGM(int index)
    {
        bgmAudio.Stop();
        bgmAudio.clip = BGM_Arr[index];
        bgmAudio.Play();
    }

    public void SavePuzzle()
    {
        SaveData savePuzzle = new SaveData(bChessClear, bMovePuzzleClear, bOnIsland);
        SaveSystem.Save(savePuzzle, fileName);
    }
}