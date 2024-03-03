using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    public GroupStorage groupStorage;
    public GroupID groupID;    

    void Start()
    {
        InstantiateGroupItems();
    }

    void InstantiateGroupItems()
    {
        List<GameObject> groupItems = groupStorage.GetBoards(groupID);

        if (groupItems != null)
        {
            foreach (GameObject item in groupItems)
            {
                GameObject instantiatedItem = Instantiate(item, transform);
                RectTransform rectTransform = instantiatedItem.GetComponent<RectTransform>();
            }
        }
    }

    public void CheckBoardCompletion()
    {
        foreach (Transform child in transform)
        {
            Board board = child.GetComponent<Board>();
            if (board != null && !board.IsPuzzleComplete)
            {
                return;
            }
        }

        if (groupID != groupStorage.GetLastGroupID())
        {
            Stage.Instance.GroupCompleted();
        }

        else
        {
            Stage.Instance.StageCompleted();
        }
        
    }
}
