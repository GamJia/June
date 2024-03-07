using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PrologueManager : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) == "true")
        {
            GetComponent<PlayableDirector>().enabled = false;
            CinemachineManager.Instance.ChangeTarget(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "false");
            string filePath = $"{Application.persistentDataPath}/PuzzleData.json";
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                Debug.Log("PuzzleData.json deleted successfully.");
            }
        }
    }
}
