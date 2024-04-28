using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class DialogueDataManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string key = SceneManager.GetActiveScene().name + "_Dialogue";

        if (PlayerPrefs.HasKey(key))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveDialogueData()
    {
        string key = SceneManager.GetActiveScene().name + "_Dialogue";
        PlayerPrefs.SetString(key, "Completed");
        PlayerPrefs.Save();
    }
}
