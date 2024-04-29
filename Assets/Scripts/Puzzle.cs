using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using System;
using MoreMountains.NiceVibrations;

public class Puzzle : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector2 originalPosition;
    private Vector2 originalSize; 
    private Vector2 currentSize;
    private RectTransform rectTransform;
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

        var puzzlesData = PuzzleDataManager.LoadPuzzleStates();
        var puzzleData = puzzlesData.Find(p => p.puzzleName == gameObject.name);
        if (puzzleData != null)
        {
            isSolved = puzzleData.isSolved;
            if(isSolved)
            {
                Board targetBoard = Board.FindBoardByBoardID(boardID);
                if (targetBoard != null)
                {                
                    targetBoard.CorrectPuzzle(this.gameObject, originalPosition,originalSize,true);
                }

            }
        }

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

        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 3;

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
        int currentGaugeValue = PlayerPrefs.GetInt("CurrentGaugeValue", 0);

        ScrollView.Instance.ResetAlpha(this.gameObject,parentTransform);

        Canvas canvas = gameObject.GetComponent<Canvas>();
        if (canvas != null)
        {
            Destroy(canvas);
        }

        if (distance <= 40f&& currentGaugeValue > 0)
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

            PuzzleDataManager.SavePuzzleStates(puzzlesData);
            MMVibrationManager.Vibrate();
            AudioManager.Instance.PlaySFX(AudioID.Correct);  
            GaugeBar.Instance.UpdateGauge();
  
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

                    
                    float newWidth = answerRectTransform.sizeDelta.x + 50;
                    float newHeight = answerRectTransform.sizeDelta.y + 50;

                    if(newWidth<100)
                    {
                        newWidth=100;
                    }

                    if(newHeight<100)
                    {
                        newHeight=100;
                    }                    

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
    
}


