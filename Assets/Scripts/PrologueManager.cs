using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PrologueManager : MonoBehaviour
{

    public void DeleteData()
    {
        PlayerPrefs.SetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "false");
        
        PlayerPrefs.DeleteKey("IsItemAvailable");
        PlayerPrefs.DeleteKey("ItemQuantity");
        PlayerPrefs.DeleteKey("LastItemTime");

        string filePath = $"{Application.persistentDataPath}/PuzzleData.json";
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            Debug.Log("PuzzleData.json deleted successfully.");
        }

    }
}
