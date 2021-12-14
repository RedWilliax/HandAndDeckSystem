using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HAD_CardCreatorWindow : EditorWindow
{
    static DataCard dataCard;
    static StatCard newStat;

    Vector2 scrollStats;


    [MenuItem("Tools/Card/CardCreator")]
    static void Init()
    {
        HAD_CardCreatorWindow window = (HAD_CardCreatorWindow)GetWindow(typeof(HAD_CardCreatorWindow));

        window.Show();

        dataCard = new DataCard();

        dataCard.StatCards = new List<StatCard>();

        newStat = new StatCard();

    }

    private void OnGUI()
    {

        SetName();

        EditorGUILayout.Space();

        SetRarity();

        EditorGUILayout.Space();

        scrollStats = EditorGUILayout.BeginScrollView(scrollStats);

        StatsManager();

        EditorGUILayout.Space();

        AddStat();

        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();

        if (GUILayout.Button("Create Card"))
        {
            //CreateCard(cardtoCreate);
        }
    }

    #region CardCreator

    void SetName()
    {
        dataCard.Name = EditorGUILayout.TextField("Card's Name :", dataCard.Name);
    }

    void SetRarity()
    {
        dataCard.Rarity = (ERarity)EditorGUILayout.EnumPopup("Rarity's Card : ", dataCard.Rarity);
    }


    void StatsManager()
    {
        for (int i = 0; i < dataCard.StatCards.Count; i++)
        {
            StatCard _currentStat = dataCard.StatCards[i];

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            _currentStat.Stat = (ECardStat)EditorGUILayout.EnumPopup("Type's Stat : ", _currentStat.Stat);

            _currentStat.Data = EditorGUILayout.FloatField("Data's Sata : ", _currentStat.Data);

            EditorGUILayout.EndVertical();

            dataCard.StatCards[i] = _currentStat;

            if (GUILayout.Button(" X "))
                dataCard.RemoveStat(_currentStat);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }
    }

    void AddStat()
    {
        newStat.Stat = (ECardStat)EditorGUILayout.EnumPopup("Type's Stat : ", newStat.Stat);

        newStat.Data = EditorGUILayout.FloatField("Data's Sata : ", newStat.Data);

        if (GUILayout.Button(" Add Stat "))
            dataCard.AddStat(newStat);
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
