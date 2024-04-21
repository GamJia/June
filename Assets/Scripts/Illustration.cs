using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Illustration : MonoBehaviour
{
    private GameObject illustration; 

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    public void SetIllustration(int index)
    {
        Image background = GetComponent<Image>(); 
        background.enabled = true; 
        if (illustration != null)
        {
            illustration.SetActive(false); 
        }

        illustration = transform.GetChild(index).gameObject;
        illustration.SetActive(true);
    }
}
