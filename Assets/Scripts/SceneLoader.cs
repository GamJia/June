using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int sceneIndex; 
    void Start()
    {
        if (PlayerPrefs.HasKey("SavedSceneIndex"))
        {
            GetComponentInChildren<Text>().text = "이어하기";
        }
        else
        {
            GetComponentInChildren<Text>().text = "처음부터";
        }

    }

    // public void ResetSavedScene() 튜토리얼 영상용
    // {
    //     if (PlayerPrefs.HasKey("SavedSceneIndex"))
    //     {
    //         PlayerPrefs.DeleteKey("SavedSceneIndex");
    //         Debug.Log("삭제 성공");
    //     }
    //     else
    //     {
    //         Debug.Log("이미 지워짐!");
    //     }
    // }

    public void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey("SavedSceneIndex"))
        {
            int savedSceneIndex = PlayerPrefs.GetInt("SavedSceneIndex");
            PlayerPrefs.SetInt("NextSceneIndex", savedSceneIndex);
            SceneManager.LoadScene(1); 
        }

        else
        {
            PlayerPrefs.SetInt("NextSceneIndex", 2);
            SceneManager.LoadScene(1); 
        }
    }

    public void LoadNextScene()
    {
        int totalScenes = SceneManager.sceneCountInBuildSettings;
        if (sceneIndex >= totalScenes)
        {
            sceneIndex = 0; 
        }

        PlayerPrefs.SetInt("NextSceneIndex", sceneIndex);
        SceneManager.LoadScene(1);
    }
}
