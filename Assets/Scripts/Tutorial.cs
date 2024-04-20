using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class Tutorial : MonoBehaviour, IPointerClickHandler
{
    private int clickCount = 0; 
    [SerializeField] private GameObject illustrationButton;

    private void Start()
    {
        UpdateChildren();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;
        UpdateChildren(); 
    }

    private void UpdateChildren()
    {
        if (clickCount < transform.childCount)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(i == clickCount);
            }
        }

        else
        {
            gameObject.SetActive(false); 
        }

    }

}
