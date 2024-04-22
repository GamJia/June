using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.IO;

public enum StageType
{
    Tutorial,
    MainGame
}

public class Stage : MonoBehaviour
{
    [SerializeField] private GameObject puzzleGameObject;
    public StageType currentStageType;

    public BoardStorage boardStorage;
    public GroupStorage groupStorage;
    public GroupID groupID= GroupID.Group_0; 
    private List<GameObject> boards = new List<GameObject>();
    private List<GameObject> puzzles = new List<GameObject>();

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
            boards = groupStorage.GetGroup(groupID); 
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

    public void LoadPuzzle()
    {
        string key = SceneManager.GetActiveScene().name + "StageType";

        if (PlayerPrefs.HasKey(key))
        {
            currentStageType = (StageType)PlayerPrefs.GetInt(key);
        }

        puzzles.Clear();

        if (groupStorage != null)
        {
            boards = groupStorage.GetGroup(groupID);
            foreach (GameObject boardObject in boards)
            {
                Board boardComponent = boardObject.GetComponent<Board>();
                if (boardComponent != null)
                {
                    List<GameObject> boardPuzzles = boardStorage.GetBoard(boardComponent.boardID);
                    puzzles.AddRange(boardPuzzles);
                }
            }

            RectTransform rectTransform = GetComponent<RectTransform>();

            switch (currentStageType)
            {
                case StageType.Tutorial:
                    puzzles = puzzles.GetRange(0, 7); 
                    if(rectTransform != null)
                    {
                        rectTransform.pivot = new Vector2(0.5f, 1f);
                    }
                    break;
                case StageType.MainGame:
                    Shuffle(puzzles);

                    if(rectTransform != null)
                    {
                        rectTransform.pivot = new Vector2(0, 1f);
                    }
                    break;
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
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        foreach (GameObject puzzle in puzzles)
        {
            GameObject puzzleParentInstance = Instantiate(puzzleGameObject, this.transform.position, Quaternion.identity, this.transform);
            GameObject puzzleInstance = Instantiate(puzzle, puzzle.transform.position, puzzle.transform.rotation);

            RectTransform puzzleRectTransform = puzzleInstance.GetComponent<RectTransform>();
            BoardID boardID = boardStorage.GetPuzzle(puzzle);

            switch (currentStageType)
            {
                case StageType.Tutorial:
                    TutorialPuzzle tutorialPuzzle = puzzleInstance.GetComponent<TutorialPuzzle>();
                    if (tutorialPuzzle == null)
                    {
                        tutorialPuzzle = puzzleInstance.AddComponent<TutorialPuzzle>();
                    }
                    tutorialPuzzle.boardID = boardID;
                    break;
                case StageType.MainGame:
                    Puzzle puzzleComponent = puzzleInstance.GetComponent<Puzzle>();
                    if (puzzleComponent == null)
                    {                                              
                        puzzleComponent = puzzleInstance.AddComponent<Puzzle>();
                    }
                    puzzleComponent.boardID = boardID;
                    break;
            }
            

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


    public void ChangeStageType()
    {
        if (currentStageType == StageType.Tutorial)
        {
            currentStageType = StageType.MainGame;
        }
        else
        {
            currentStageType = StageType.Tutorial;
        }

        SaveStageType();
        LoadPuzzle();
    }

    private void SaveStageType()
    {
        string key = SceneManager.GetActiveScene().name + "StageType";
        PlayerPrefs.SetInt(key, (int)currentStageType);
        PlayerPrefs.Save();
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