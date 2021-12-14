using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HAD_CardCreatorWindow : EditorWindow
{
    HAD_Card cardtoCreate = null;

    [MenuItem("Tools/Card/CardCreator")]
    static void Init()
    {
        HAD_CardCreatorWindow window = (HAD_CardCreatorWindow)GetWindow(typeof(HAD_CardCreatorWindow));

        window.Show();

    }
    private void OnGUI()
    {
        

        EditorGUILayout.Space();


        if (GUILayout.Button("Create Card"))
        {
            CreateCard(cardtoCreate);
        }
    }

    #region CardCreator

    void AddStat(StatCard _stat)
    {
        
    }


    #endregion


    void CreateCard(HAD_Card _cardToCreate)
    {
        Debug.Log("CreateCard");

        if (_cardToCreate)
        {
            Debug.LogError("Card to create is null");
            return;
        }

    }


}
