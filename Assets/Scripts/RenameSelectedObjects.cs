using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RenameSelectedObjects : ScriptableObject
{
    [MenuItem("Tools/Rename Selected Objects")]
    static void RenameObjects()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            string originalName = obj.name;
            if (originalName.StartsWith("0_"))
            {
                string[] parts = originalName.Split('_');
                if (parts.Length == 2 && int.TryParse(parts[1], out int number))
                {
                    obj.name = "0_" + (number + 80).ToString();
                }
            }
        }
    }
}