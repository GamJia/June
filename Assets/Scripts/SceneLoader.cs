using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    
    void Start()
    {
        Text continueText = GameObject.Find("Continue Text")?.GetComponent<Text>();
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex.Equals(0))
        {
            if (PlayerPrefs.HasKey("SavedSceneName"))
            {
                continueText.text = "이어하기";
            }
            else
            {
                continueText.text = "처음부터";
            }
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

    public void IntroSceneButton()
    {
        if (PlayerPrefs.HasKey("SavedSceneName"))
        {
            GetComponentInChildren<Text>().text = "이어하기";
        }
        else
        {
            GetComponentInChildren<Text>().text = "처음부터";
        }
    }

    public void LoadSavedScene()
    {
        if (PlayerPrefs.HasKey("SavedSceneName"))
        {
            string savedSceneName = PlayerPrefs.GetString("SavedSceneName");
            PlayerPrefs.SetString("NextSceneName", savedSceneName);
            SceneManager.LoadScene("Loading");  
        }

        else
        {
            PlayerPrefs.SetString("NextSceneName", "Stage_0"); 
            SceneManager.LoadScene("Loading"); 
        }
    }

    public void LoadCustomScene(string sceneName)
    {
        PlayerPrefs.SetString("NextSceneName", sceneName);
        SceneManager.LoadScene("Loading");
    }

}
