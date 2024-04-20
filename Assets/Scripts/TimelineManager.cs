using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem; 
using UnityEngine.Playables;
using UnityEngine;
using System.IO;

public class TimelineManager : MonoBehaviour
{
    private PlayableDirector tutorialDirector; 

    public void Start()
    {
        tutorialDirector = GetComponent<PlayableDirector>(); 

        string filePath = $"{Application.persistentDataPath}/PuzzleData.json";

        if (File.Exists(filePath))
        {
            string fileContents = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(fileContents) && fileContents != "null")
            {
                if (tutorialDirector != null)
                {
                    tutorialDirector.enabled = false;
                    CinemachineManager.Instance.ChangeTarget(true);
                }
            }
        }

    }

    // public void StartQuest()
    // {
    //     StartCoroutine(CheckQuest());
    // }
    
    // private IEnumerator CheckQuest()
    // {
    //     string filePath = $"{Application.persistentDataPath}/PuzzleData.json";
        
    //     while (true)
    //     {
    //         if (File.Exists(filePath))
    //         {
    //             string fileContents = File.ReadAllText(filePath);
    //             if (!string.IsNullOrEmpty(fileContents) && fileContents != "null")
    //             {
    //                 // If the content of the file is not null, change the quest to a success state and exit the loop
    //                 DialogueLua.SetVariable("IsTutorialComplete", "True");
    //                 DialogueManager.StartConversation("Tutorial",null,null,12);
    //                 Debug.Log("Tutorial quest set to Success because PuzzleData is not null.");
    //                 break; 
    //             }
    //         }
            
    //         // Check every 1 second
    //         yield return new WaitForSeconds(0.5f);
    //     }
    // }
}
