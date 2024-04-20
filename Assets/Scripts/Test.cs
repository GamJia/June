using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateChildren());
    }

    // Coroutine to activate child objects one by one, each second
    IEnumerator ActivateChildren()
    {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            // Activate the current child
            transform.GetChild(i).gameObject.SetActive(true);
            // Wait for 1 second
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
