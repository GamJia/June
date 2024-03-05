using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundRaycastDisabler : MonoBehaviour
{
    void Start()
    {
        DisableRaycastTargetsInChildren();
    }

    private void DisableRaycastTargetsInChildren()
    {
        var images = GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            image.raycastTarget = false;
        }
    }
}
