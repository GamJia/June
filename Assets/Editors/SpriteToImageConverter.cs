using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SpriteToImageConverter : EditorWindow
{
    [MenuItem("Tools/Sprite to Image Converter")]
    public static void ShowWindow()
    {
        GetWindow<SpriteToImageConverter>("Sprite to Image Converter");
    }

    void OnGUI()
    {
        if (GUILayout.Button("Convert SpriteRenderers to Images"))
        {
            ConvertSpriteRendererToImage();
        }
    }

    private static void ConvertSpriteRendererToImage()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Image image = obj.GetComponent<Image>();
                if (image == null)
                {
                    image = obj.AddComponent<Image>();
                    image.sprite = spriteRenderer.sprite;
                    image.rectTransform.sizeDelta = new Vector2(spriteRenderer.sprite.bounds.size.x * 100, spriteRenderer.sprite.bounds.size.y * 100);
                    DestroyImmediate(spriteRenderer);
                }
            }
        }
    }
}
