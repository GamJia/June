using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PrologueManager : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name) == "true")
        {
            GetComponent<PlayableDirector>().enabled = false;
        }
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         PlayerPrefs.SetString("Prologue_" + UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, "false");
    //     }
    // }
}
