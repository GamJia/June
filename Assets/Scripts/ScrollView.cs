using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    public static ScrollView Instance => instance;
    private static ScrollView instance;    
    private List<Image> childImages = new List<Image>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void SetAlpha(GameObject puzzle)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0.1f;

        puzzle.transform.SetParent(transform.parent);
    }

    public void ResetAlpha(GameObject puzzle,Transform parentTransform)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        puzzle.transform.SetParent(parentTransform);
    }

    


}
