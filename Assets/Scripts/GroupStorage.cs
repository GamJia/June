using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GroupID
{
    Group_0,
    Group_1,
    Group_2,
    Group_3,
    Group_4,
    Group_5,
    Group_6,
    Group_7,
    Group_8,
    Group_9,
    Group_10,
}

[CreateAssetMenu(fileName = "GroupStorage", menuName = "Puzzle/Group Storage")]
public class GroupStorage : ScriptableObject
{
    public static GroupStorage Instance => instance;
    private static GroupStorage instance;
    
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }

        GenerateDictionary(); 
    }
    
    [SerializeField] GroupArray[] groupArray; 

    Dictionary<GroupID, List<GameObject>> groupDictionary = new Dictionary<GroupID, List<GameObject>>(); 

    void GenerateDictionary()
    {
        for (int i = 0; i < groupArray.Length; i++)
        {
            groupDictionary.Add(groupArray[i].groupID, groupArray[i].boardList); 
        }
    }

    public List<GameObject> GetBoards(GroupID id) 
    {

        if (groupDictionary.Count.Equals(0))
        {
            GenerateDictionary();
        }

        if (groupDictionary.ContainsKey(id))
        {
            return groupDictionary[id];
        }
        else
        {
            return null;
        }
    }

    public GroupID GetLastGroupID()
    {
        if (groupArray.Length.Equals(0))
        {
            throw new InvalidOperationException("No groups defined.");
        }

        int lastIndex = groupArray.Length - 1;
        return groupArray[lastIndex].groupID;
    }
}

[Serializable]
public struct GroupArray 
{
    [SerializeField] GroupID _groupID; 
    [SerializeField] List<GameObject> _boardList; 

    public GroupID groupID { get { return _groupID; } } 
    public List<GameObject> boardList { get { return _boardList; } } 
}
