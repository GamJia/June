using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; 
using System;
using MoreMountains.NiceVibrations;

public class ScrollbarManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CinemachineManager.SetPuzzleDragging(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CinemachineManager.SetPuzzleDragging(false);
    }
}
