using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            GetComponentInChildren<Text>().text = "이어하기";
        }
        else
        {
            GetComponentInChildren<Text>().text = "처음부터";
        }

    }

    public void OnButtonClick()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("SavedScene"));
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
