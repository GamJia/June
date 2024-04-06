using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem; 

public class TutorialBoard : MonoBehaviour
{
    // Start is called before the first frame update
    public int total;

    void Start()
    {
        total=0;
    }


    void OnEnable()
    {
        Lua.RegisterFunction("CountPuzzle", this, SymbolExtensions.GetMethodInfo(() => CountPuzzle((double)0)));
        
    }

    void OnDisable()
    {
        Lua.UnregisterFunction("CountPuzzle");
    }

    public void CountPuzzle(double count)
    {
        total = transform.childCount;       
    }
    
}
