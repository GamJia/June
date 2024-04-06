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
    public bool isSolved;


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

        var puzzlesData = PuzzleDataManager.LoadPuzzleStates();
        var puzzleData = puzzlesData.Find(p => p.puzzleName == gameObject.name);
        if (puzzleData != null)
        {
            isSolved = puzzleData.isSolved;
            if(isSolved)
            {
                if (transform.parent != null)
                {
                    transform.parent.gameObject.SetActive(false);
                }
                Board targetBoard = Board.FindBoardByBoardID(boardID);
                if (targetBoard != null)
                {                
                    targetBoard.CorrectPuzzle(this.gameObject, originalPosition,originalSize);
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
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0); 
            }
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        Vector3 currentPosition = new Vector3(mousePosition.x * 100, mousePosition.y * 100, 0);

        float distance = Vector3.Distance(currentPosition, originalPosition);
        float currentGaugeValue = PlayerPrefs.GetFloat("CurrentGaugeValue", 0);

        if (distance <= 30f&& currentGaugeValue > 0)
        {
            if (transform.parent != null)
            {
                transform.parent.gameObject.SetActive(false);
            }
            Board targetBoard = Board.FindBoardByBoardID(boardID);
            if (targetBoard != null)
            {                
                targetBoard.CorrectPuzzle(this.gameObject, originalPosition,originalSize);
            }

            isSolved = true;
            var puzzlesData = PuzzleDataManager.LoadPuzzleStates();
            var puzzleData = puzzlesData.Find(p => p.puzzleName == gameObject.name);

            if (puzzleData == null)
            {
                puzzlesData.Add(new PuzzleData { puzzleName = gameObject.name, isSolved = true });
            }

            else
            {
                puzzleData.isSolved = true;
            }
            PuzzleDataManager.SavePuzzleStates(puzzlesData);
            MMVibrationManager.Vibrate();
            GaugeManager.Instance.UpdateGauge();
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


    }
    
}


