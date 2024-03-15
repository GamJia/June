using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BoardID
{
    Board_0,
    Board_1,
    Board_2,
    Board_3,
    Board_4,
    Board_5,
    Board_6,
    Board_7,
    Board_8,
    Board_9,
    Board_10,
    Board_11,
    Board_12,
    Board_13,
    Board_14,
    Board_15,
    Board_16,
    Board_17,
    Board_18,
    Board_19,
    Board_20,
}

[CreateAssetMenu(fileName = "BoardStorage", menuName = "Puzzle/Board Storage")]
public class BoardStorage : ScriptableObject
{
    public static BoardStorage Instance => instance;
    private static BoardStorage instance;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        GenerateDictionary(); 
    }
    
    public BoardArray[] boardArray; 

    Dictionary<BoardID, List<GameObject>> boardDictionary = new Dictionary<BoardID, List<GameObject>>(); 

    void GenerateDictionary()
    {
        for (int i = 0; i < boardArray.Length; i++)
        {
            boardDictionary.Add(boardArray[i].boardID, boardArray[i].puzzleList);
        }
    }

    public List<GameObject> GetBoard(BoardID id) 
    {

        if (boardDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (boardDictionary.ContainsKey(id))
        {
            return boardDictionary[id];
        }

        else
        {
            return null;
        }
    }

}

[Serializable]
public struct BoardArray 
{
    [SerializeField] BoardID _boardID; 
    [SerializeField] List<GameObject> _puzzleList; 

    public BoardID boardID { get { return _boardID; } } 
    public List<GameObject> puzzleList { get { return _puzzleList; } } 
}
