using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HAD_CardRarityColor : EditorWindow
{
    public static Color COMMON;
    public static Color RARE;
    public static Color EPIC;
    public static Color LEGENDARY;
    public static Color UNIQUE;

    [MenuItem("Tools/Card/Color Rarity")]
    static void Init()
    {
        HAD_CardRarityColor _window = (HAD_CardRarityColor)GetWindow(typeof(HAD_CardRarityColor));

        _window.maxSize = new Vector2(250, 100);
        _window.minSize = new Vector2(250, 100);

        _window.Show();

    }


    private void OnGUI()
    {
        COMMON = EditorGUILayout.ColorField("Common : ", COMMON);
        RARE = EditorGUILayout.ColorField("Rare : ", RARE);
        EPIC = EditorGUILayout.ColorField("Epic : ", EPIC);
        LEGENDARY = EditorGUILayout.ColorField("Legendary : ", LEGENDARY);
        UNIQUE = EditorGUILayout.ColorField("Unique : ", UNIQUE);
    }


}
