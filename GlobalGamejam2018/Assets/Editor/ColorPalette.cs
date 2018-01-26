using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[Serializable]
public class ColorPalette : ScriptableObject
{
    #if UNITY_EDITOR
    [MenuItem("Assets/Create/Colo Palette")]
    public static void CreateCollorPalette()
    {
        if(Selection.activeObject is Texture2D)
        {
            Texture2D selectedTexture = Selection.activeObject as Texture2D;
            string selectionPath = AssetDatabase.GetAssetPath(selectedTexture);

            selectionPath = selectionPath.Replace(".png", "-color-palette.asset");

            ColorPalette newPalette = CustomAssetUtil.CreateAsset<ColorPalette>(selectionPath);

            newPalette.source = selectedTexture;
            newPalette.ResetPalette();

            Debug.Log("Created a Palette " + selectionPath);
        }
        else
        {
            Debug.Log("Failed to create a Color Palette");
        }
    }
    #endif

    public Texture2D source;
    public List<Color> palette = new List<Color>();
    public List<Color> newPalette = new List<Color>();
    public Texture2D cachedTexture;

    private List<Color> BuildPalette(Texture2D texture)
    {
        List<Color> palette = new List<Color>();

        Color[] colors = texture.GetPixels();

        foreach (Color color in colors)
        {
            if(!palette.Contains(color))
            {
                if(color.a == 1)
                {
                    palette.Add(color);
                }
            }
        }

        return palette;
    }

    public void ResetPalette()
    {
        palette = BuildPalette(source);
        newPalette = new List<Color>(palette);
    }

    public Color GetColor(Color color)
    {
        for(int i = 0; i < palette.Count; i++)
        {
            Color tempColor = palette[i];
               
            if(Mathf.Approximately(color.r, tempColor.r) && Mathf.Approximately(color.g, tempColor.g) && Mathf.Approximately(color.b, tempColor.b) && Mathf.Approximately(color.a, tempColor.a))
            {
                return newPalette[i];
            }
        }

        return color;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorPalette))]
public class ColorPaletteEditor : Editor
{
    public ColorPalette colorPalette;

    void OnEnable()
    {
        colorPalette = target as ColorPalette;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Source Texture");

        colorPalette.source = EditorGUILayout.ObjectField(colorPalette.source, typeof(Texture2D), false) as Texture2D;

        EditorGUILayout.BeginHorizontal();

        GUILayout.Label("Current Colors");
        GUILayout.Label("New Colors");

        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < colorPalette.palette.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.ColorField(colorPalette.palette[i]);
            colorPalette.newPalette[i] = EditorGUILayout.ColorField(colorPalette.newPalette[i]);

            EditorGUILayout.EndHorizontal();
        }

        if(GUILayout.Button("Revert Pallette"))
        {
            colorPalette.ResetPalette();
        }

        EditorUtility.SetDirty(colorPalette);
    }
}
#endif