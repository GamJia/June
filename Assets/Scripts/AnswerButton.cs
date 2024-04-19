using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// public enum ItemType
// {
//     Answer,
//     Test,
// }

public class AnswerButton : MonoBehaviour
{
    private Coroutine itemCoroutine;
    private Text countText;
    private Text timerText;
    [SerializeField] private Sprite[] itemIcons;

    void Awake()
    {
        countText = transform.GetChild(0).GetComponent<Text>();
        timerText = transform.GetChild(1).GetComponent<Text>();
    }

    void Start()
    {
        InitializeItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeItems()
    {
        Image itemIcon=GetComponent<Image>();

        countText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);

        float itemQuantity = PlayerPrefs.GetFloat("ItemQuantity", 0);
        int isItemAvailable = PlayerPrefs.GetInt("IsItemAvailable", 0);
        int lastItemTime = PlayerPrefs.GetInt("LastItemTime", 30); 

        if(lastItemTime>0&&isItemAvailable.Equals(1))
        {
            if (itemCoroutine != null)
            {
                StopCoroutine(itemCoroutine);
            }
            itemCoroutine = StartCoroutine(ItemCoroutine(lastItemTime));

        }

        if (itemQuantity >= 1)
        {
            countText.gameObject.SetActive(true);
            itemIcon.sprite = itemIcons[1]; 
            if (itemQuantity >= 9)
            {
                countText.text = "9+";
            }
            else
            {
                countText.text = itemQuantity.ToString();
            }
        }      
        
        else
        {
            countText.gameObject.SetActive(false);
            itemIcon.sprite = itemIcons[0]; 
        }

        
    }

    public void TutorialItem()
    {
        float itemQuantity=PlayerPrefs.GetFloat("ItemQuantity",0);
        PlayerPrefs.SetFloat("ItemQuantity", itemQuantity+1);
        PlayerPrefs.SetInt("IsItemAvailable", 1); 

        countText.gameObject.SetActive(true);
        countText.text = "1";

        timerText.gameObject.SetActive(true);
        timerText.text="99:99";

        Image itemIcon=GetComponent<Image>();
        itemIcon.sprite = itemIcons[1]; 
    }

    public void AddItem()
    {
        float itemQuantity=PlayerPrefs.GetFloat("ItemQuantity",0);
        PlayerPrefs.SetFloat("ItemQuantity", itemQuantity+1);

        StartItemCoroutine();
    }

    public void StartItemCoroutine()
    {   
        InitializeItems();

        float itemQuantity=PlayerPrefs.GetFloat("ItemQuantity",0);
        if(itemQuantity>0)
        {
            if(itemCoroutine!=null)
            {
                StopCoroutine(itemCoroutine);
            }
            
            itemCoroutine=StartCoroutine(ItemCoroutine(60));

        }
    }

    private IEnumerator ItemCoroutine(int remainingTime)
    {    
        float elapsedTime=0;
        timerText.gameObject.SetActive(true);
        PlayerPrefs.SetInt("IsItemAvailable", 1);
        PlayerPrefs.SetFloat("ItemQuantity", Mathf.Max(0, PlayerPrefs.GetFloat("ItemQuantity", 0) - 1));

        while(elapsedTime<remainingTime)
        {
            int timeLeft = Mathf.Max(0, (int)(remainingTime - elapsedTime));
            timerText.text = $"0:{timeLeft:D2}";
            PlayerPrefs.SetInt("LastItemTime", timeLeft); 
            yield return new WaitForSeconds(1f); 
            elapsedTime++;
        }

        PlayerPrefs.SetInt("IsItemAvailable", 0); 
        CinemachineManager.Instance.ItemTarget(null);
        timerText.gameObject.SetActive(false);

        InitializeItems();
    }


}
