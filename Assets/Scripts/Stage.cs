using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject puzzleGameObject;
    public BoardStorage boardStorage;
    public GroupStorage groupStorage;
    public GroupID groupID= GroupID.Group_0; 
    [SerializeField] private List<GameObject> boards = new List<GameObject>();
    [SerializeField] private List<GameObject> puzzles = new List<GameObject>();

    public static Stage Instance => instance;
    private static Stage instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if(groupStorage != null)
        {
            boards = groupStorage.GetBoards(groupID); 
            foreach (GameObject boardObject in boards)
            {
                boardObject.GetComponent<Image>().color = new Color(0.3f,0.3f,0.3f,1f);

            }
        }
        
    }

    void Start()
    {
        LoadPuzzle();
    }

    void LoadPuzzle()
    {
        puzzles.Clear();
        if (groupStorage != null)
        {
            boards = groupStorage.GetBoards(groupID);
            foreach (GameObject boardObject in boards)
            {
                Board boardComponent = boardObject.GetComponent<Board>();
                if (boardComponent != null)
                {
                    List<GameObject> boardPuzzles = boardStorage.GetBoard(boardComponent.boardID);
                    puzzles.AddRange(boardPuzzles);
                }
            }
            
            if (SceneManager.GetActiveScene().buildIndex.Equals(1)) // For Prologue
            {
                if (puzzles.Count > 1)
                {
                    GameObject firstPuzzle = puzzles[0];
                    puzzles.RemoveAt(0);
                    Shuffle(puzzles); 
                    puzzles.Insert(0, firstPuzzle); 
                }
            }

            else
            {
                Shuffle(puzzles);
            }

            AssignPuzzle();
        }
    }

   
    private void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void AssignPuzzle()
    {
        foreach (GameObject puzzle in puzzles)
        {
            GameObject puzzleParentInstance = Instantiate(puzzleGameObject, this.transform.position, Quaternion.identity, this.transform);
            GameObject puzzleInstance = Instantiate(puzzle, puzzle.transform.position, puzzle.transform.rotation);

            RectTransform puzzleRectTransform = puzzleInstance.GetComponent<RectTransform>();
            if (puzzleRectTransform != null)
            {
                float width = puzzleRectTransform.rect.width;
                float height = puzzleRectTransform.rect.height;

                float targetSize = 180f;

                if (width >= height)
                {
                    puzzleRectTransform.sizeDelta = new Vector2(targetSize, height * (targetSize / width));
                }
                else 
                {
                    puzzleRectTransform.sizeDelta = new Vector2(width * (targetSize / height), targetSize);
                }

                puzzleRectTransform.pivot = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                puzzleRectTransform.anchoredPosition = Vector2.zero;
            }

            puzzleInstance.transform.SetParent(puzzleParentInstance.transform, false);
        }
    }

    public void GroupCompleted()
    {
        groupID++; 
        LoadPuzzle();
    }

    public void StageCompleted()
    {

    }

    
}
