using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum ItemType
// {
//     Answer,
//     Test,
// }

public class Item : MonoBehaviour
{
    private Coroutine itemCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialItem()
    {
        float itemQuantity=PlayerPrefs.GetFloat("ItemQuantity",0);
        PlayerPrefs.SetFloat("ItemQuantity", itemQuantity+1);
        
        StartItemCoroutine();
    }

    public void StartItemCoroutine()
    {
        float itemQuantity=PlayerPrefs.GetFloat("ItemQuantity",0);
        if(itemQuantity>0)
        {
            if(itemCoroutine!=null)
            {
                StopCoroutine(ItemCoroutine());
            }
            
            PlayerPrefs.SetFloat("ItemQuantity", itemQuantity-1);
            PlayerPrefs.SetInt("IsItemAvailable", 1); 
            itemCoroutine=StartCoroutine(ItemCoroutine());
        }
    }

    private IEnumerator ItemCoroutine()
    {    
        float elapsedTime=0;
        while(elapsedTime<30f)
        {
            yield return new WaitForSeconds(1f);
            elapsedTime++;
        }

        PlayerPrefs.SetInt("IsItemAvailable", 0); 
    }





}
