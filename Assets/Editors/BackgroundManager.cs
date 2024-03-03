using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] GameObject[] backgrounds; 
    private GameObject background = null;
    private int index=0;

    public void UpdateBackground()
    {
        if(background!=null)
        {
            Destroy(background);
        }

        background=Instantiate(backgrounds[index],transform);
        RectTransform rectTransform = background.GetComponent<RectTransform>();

        if(index < backgrounds.Length - 1)
        {
            index++;
        }
        
        else
        {
            index = 0; 
        }
    }

}
