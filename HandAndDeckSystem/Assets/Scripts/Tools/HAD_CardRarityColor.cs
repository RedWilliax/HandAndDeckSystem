using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HAD_CardRarityColor : EditorWindow
{

    public static Color COMMON;

    [MenuItem("Tools/Card/Color Rarity")]
    static void Init()
    {
        HAD_CardRarityColor _window = (HAD_CardRarityColor)GetWindow(typeof(HAD_CardRarityColor));

        _window.Show();

    }

}
