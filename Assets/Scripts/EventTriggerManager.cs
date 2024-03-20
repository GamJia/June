using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class EventTriggerManager : EventTrigger
{
    void Awake()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>() ?? gameObject.AddComponent<EventTrigger>();
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        CinemachineManager.Instance?.SetDragging(true);
    }

    public override void OnEndDrag(PointerEventData data)
    {
        CinemachineManager.Instance?.SetDragging(false);
    }


}
