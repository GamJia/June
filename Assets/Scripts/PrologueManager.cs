using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement; 

public class PrologueManager : MonoBehaviour
{

    public void DeleteData()
    {
        PlayerPrefs.SetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "false");
        
        PlayerPrefs.DeleteKey("IsItemAvailable");
        PlayerPrefs.DeleteKey("ItemQuantity");
        PlayerPrefs.DeleteKey("LastItemTime");
        PlayerPrefs.DeleteKey("LastGaugeTime");

        string key = SceneManager.GetActiveScene().name + "_StageType";
        PlayerPrefs.DeleteKey(key);

        string dialogueKey = SceneManager.GetActiveScene().name + "_Dialogue";
        PlayerPrefs.DeleteKey(dialogueKey);

        string filePath = $"{Application.persistentDataPath}/PuzzleData.json";
        if (System.IO.File.Exists(filePath))
        {
            System.IO.File.Delete(filePath);
            Debug.Log("PuzzleData.json deleted successfully.");
        }

    }
}
