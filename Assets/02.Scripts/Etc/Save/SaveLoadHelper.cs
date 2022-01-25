using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public SaveData(bool chess, bool movePuzzle, bool onIsland)
	{
		bChessClear = chess;
        bMovePuzzleClear = movePuzzle;
        bOnIsland = onIsland;
	}

    public bool bChessClear; // 체스퍼즐 클리어 했는지
    public bool bMovePuzzleClear; // 슬라이드퍼즐 클리어 했는지
    public bool bOnIsland; // 섬을 지났는지
}

public static class SaveSystem
{
    private static string SavePath => Application.persistentDataPath + "/saves/";

    public static void Save(SaveData saveData, string saveFileName)
    {
        if (!Directory.Exists(SavePath))
        {
            Directory.CreateDirectory(SavePath);
        }

        string saveJson = JsonUtility.ToJson(saveData);

        string saveFilePath = SavePath + saveFileName + ".json";
        File.WriteAllText(saveFilePath, saveJson);
        Debug.Log("Save Success: " + saveFilePath);
    }

    public static SaveData Load(string saveFileName)
    {
        string saveFilePath = SavePath + saveFileName + ".json";

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError("No such saveFile exists");
            return null;
        }

        string saveFile = File.ReadAllText(saveFilePath);
        SaveData saveData = JsonUtility.FromJson<SaveData>(saveFile);
        return saveData;
    }
}