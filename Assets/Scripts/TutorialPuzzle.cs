using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using System;
using PixelCrushers.DialogueSystem; 

public class TutorialPuzzle : MonoBehaviour,IEndDragHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
    }

   


    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 0));
        Vector3 currentPosition = new Vector3(mousePosition.x * 100, mousePosition.y * 100, 0);

        float distance = Vector3.Distance(currentPosition, originalPosition);

        if (distance <= 30f)
        {
            int currentPuzzleComplete = DialogueLua.GetVariable("PuzzleComplete").asInt;
            currentPuzzleComplete++;
            DialogueLua.SetVariable("PuzzleComplete", currentPuzzleComplete);
            if(currentPuzzleComplete%3==0)
            {
                DialogueManager.StartConversation("Tutorial");
            }
        }
        

    }
    
}


