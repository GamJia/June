using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class PuzzleDataManager
{
    private static string SavePath => $"{Application.persistentDataPath}/PuzzleData.json";

    public static List<PuzzleData> LoadPuzzleStates()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            PuzzleDataList dataList = JsonUtility.FromJson<PuzzleDataList>(json);
            return dataList.puzzles;
        }
        return new List<PuzzleData>();
    }

    public static void SavePuzzleStates(List<PuzzleData> puzzlesData)
    {
        PuzzleDataList dataList = new PuzzleDataList { puzzles = puzzlesData };
        string json = JsonUtility.ToJson(dataList);
        File.WriteAllText(SavePath, json);
    }
}

[System.Serializable]
public class PuzzleData
{
    public string puzzleName; 
    public bool isSolved; 
}

[System.Serializable]
public class PuzzleDataList
{
    public List<PuzzleData> puzzles = new List<PuzzleData>(); 
}

