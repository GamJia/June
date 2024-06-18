using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;

    void Start()
    {
        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        string nextSceneName = PlayerPrefs.GetString("NextSceneName", "Intro");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        asyncLoad.allowSceneActivation = false;

        // Fake Timer
        float timer = 0f;
        while (timer < 3)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / 3);
            if (progressBar != null)
            {
                progressBar.value = progress;
            }
            yield return null;
        }

        while (!asyncLoad.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            }

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}
