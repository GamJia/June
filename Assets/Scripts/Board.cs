using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Board : MonoBehaviour
{
    public Vector2 currentPosition;
    private RectTransform rectTransform;
    public BoardID boardID;
    public bool IsPuzzleComplete { get; private set; }
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            currentPosition = rectTransform.anchoredPosition;
        }
    }


    public void CorrectPuzzle(GameObject puzzle, Vector2 puzzlePosition, Vector2 puzzleSize,bool isCorrect)
    {
        RectTransform puzzleRectTransform = puzzle.GetComponent<RectTransform>();
        if (puzzle.transform.parent != null)
        {
            puzzle.transform.parent.gameObject.SetActive(false);
        }
        
        if (puzzleRectTransform != null)
        {
            Vector2 adjustment = puzzlePosition - currentPosition;
            puzzleRectTransform.anchoredPosition = adjustment;
            puzzleRectTransform.sizeDelta = puzzleSize;            
            puzzleRectTransform.SetParent(this.transform, false);

            if (!isCorrect)
            {
                Animator puzzleAnimator = puzzle.GetComponent<Animator>();
                if (puzzleAnimator != null)
                {
                    puzzleAnimator.SetTrigger("IsCorrect");
                }
                //puzzleAnimator.enabled=false;

            }     
            
            Image puzzleImage = puzzleRectTransform.GetComponent<Image>();
            if (puzzleImage != null)
            {
                puzzleImage.raycastTarget = false;
            }

        }

        CheckPuzzleCompletion();
        
    }

    


    void CheckPuzzleCompletion()
    {
        List<GameObject> puzzleList = Stage.Instance.boardStorage.GetBoard(boardID);
        IsPuzzleComplete = transform.childCount.Equals(puzzleList.Count);
        if(IsPuzzleComplete)
        {
            Group targetGroup = GetComponentInParent<Group>();
            if (targetGroup != null)
            {
                targetGroup.CheckBoardCompletion();
            }
        }
        
    }

    public static Board FindBoardByBoardID(BoardID id)
    {
        Board[] boards = FindObjectsOfType<Board>();
        foreach (var board in boards)
        {
            if (board.boardID.Equals(id))
            {
                return board;
            }                
        }
        return null;
    }
}

