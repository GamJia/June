using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using System;
using PixelCrushers.DialogueSystem; 
using MoreMountains.NiceVibrations;

public class TutorialPuzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;
    private Vector2 originalSize; 
    private Vector2 currentSize;
    public BoardID boardID; 
    private bool isSolved;
    private GameObject answer;
    private Transform parentTransform;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
            originalSize = rectTransform.sizeDelta;
        }
    }

    void Start()
    {
        currentSize = rectTransform.sizeDelta;
        parentTransform = transform.parent;

        if (GetComponent<EventTriggerManager>() == null)
        {
            gameObject.AddComponent<EventTriggerManager>();
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        float currentCameraSize = Camera.main.orthographicSize;
        float sizeAdjustment = 5.4f / currentCameraSize; 
        rectTransform.sizeDelta = originalSize * sizeAdjustment;

        ScrollView.Instance.SetAlpha(this.gameObject);

        ShowAnswer();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransform canvasRectTransform = transform.parent as RectTransform; 
        if (canvasRectTransform != null)
        {
            Vector3 worldPoint;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out worldPoint))
            {
                transform.position = worldPoint;
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y+100, 0); 
            }
        }

    }

   


    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y+100, 0));
        Vector3 currentPosition = new Vector3(mousePosition.x * 100, mousePosition.y * 100, 0);

        float distance = Vector3.Distance(currentPosition, originalPosition);
        ScrollView.Instance.ResetAlpha(this.gameObject,parentTransform);

        if (distance <= 40f)
        {
            isSolved = true;
            var puzzlesData = PuzzleDataManager.LoadPuzzleStates();
            var puzzleData = puzzlesData.Find(p => p.puzzleName == gameObject.name);

            if (puzzleData == null)
            {
                puzzlesData.Add(new PuzzleData { puzzleName = gameObject.name, isSolved = true });

                Board targetBoard = Board.FindBoardByBoardID(boardID);
                if (targetBoard != null)
                {                
                    targetBoard.CorrectPuzzle(this.gameObject, originalPosition,originalSize,false);
                }
            }

            else
            {
                puzzleData.isSolved = true;
                
                Board targetBoard = Board.FindBoardByBoardID(boardID);
                if (targetBoard != null)
                {                
                    targetBoard.CorrectPuzzle(this.gameObject, originalPosition,originalSize,true);
                }
            }
            
            int currentPuzzleComplete = DialogueLua.GetVariable("PuzzleComplete").asInt;
            currentPuzzleComplete++;
            DialogueLua.SetVariable("PuzzleComplete", currentPuzzleComplete);
            if(currentPuzzleComplete%3==0)
            {
                DialogueManager.StartConversation("Tutorial");
            }

            PuzzleDataManager.SavePuzzleStates(puzzlesData);
            MMVibrationManager.Vibrate();
            AudioManager.Instance.PlaySFX(AudioID.Correct);  
        }

        else
        {
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = currentSize;
                rectTransform.anchoredPosition = Vector2.zero; 
            }
        }

        HideAnswer();

        
        
    }

    public void ShowAnswer()
    {
        if(PlayerPrefs.GetInt("IsItemAvailable", 0) == 1) 
        {
            Board targetBoard = Board.FindBoardByBoardID(boardID);
            if(targetBoard!=null&&answer==null)
            {
                answer=Instantiate(this.gameObject,targetBoard.transform);
                RectTransform answerRectTransform=answer.GetComponent<RectTransform>();
                answerRectTransform.anchoredPosition = originalPosition-targetBoard.currentPosition;
                answerRectTransform.sizeDelta = originalSize;

                Animator puzzleAnimator=answer.GetComponent<Animator>();
                if(puzzleAnimator!=null)
                {
                    puzzleAnimator.SetTrigger("IsUsingItem");
                }

                GameObject cameraPrefab = Resources.Load<GameObject>("Prefabs/Camera");
                if (cameraPrefab != null)
                {
                    GameObject cameraInstance = Instantiate(cameraPrefab, answer.transform);
                    RectTransform cameraRectTransform = cameraInstance.GetComponent<RectTransform>();

                    // Calculate the new dimensions based on the larger dimension of 'answer'
                    float newWidth = answerRectTransform.sizeDelta.x + 50;
                    float newHeight = answerRectTransform.sizeDelta.y + 50;

                    // Set the dimensions of the camera prefab
                    cameraRectTransform.sizeDelta = new Vector2(newWidth, newHeight);
                }

                CinemachineManager.Instance.ItemTarget(answer);
                
            }        
        }
        
        
    }

    public void HideAnswer()
    {
        if(answer!=null)
        {
            Destroy(answer);
        }
        CinemachineManager.Instance.ItemTarget(null);
        
    }

    void OnApplicationQuit()
    {
        int puzzleCompleteCount = DialogueLua.GetVariable("PuzzleComplete").asInt;
        if (puzzleCompleteCount < 6)
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
    
}


