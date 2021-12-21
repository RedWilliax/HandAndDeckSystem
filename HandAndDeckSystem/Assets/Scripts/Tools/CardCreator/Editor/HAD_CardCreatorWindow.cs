using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using JsonCom;

public class HAD_CardCreatorWindow : EditorWindow
{
    static DataCard dataCard;
    static StatCard newStat;
    static ListCardData allCards;

    Vector2 scrollStats;
    Sprite sprite = null;
    bool canCreate = false;

    [MenuItem("Tools/Card/CardCreator")]
    static void Init()
    {
        HAD_CardCreatorWindow window = (HAD_CardCreatorWindow)GetWindow(typeof(HAD_CardCreatorWindow));

        window.Show();

        ResetTool();

    }

    static void ResetTool()
    {
        allCards = HAD_PathHelper.GetCardsDataBase();

        dataCard = new DataCard();

        dataCard.ListStats = new ListStatCard(new List<StatCard>());

        newStat = new StatCard();
    }

    private void OnGUI()
    {
        SetName();

        EditorGUILayout.Space();

        SetSprite();

        EditorGUILayout.Space();
        SetTypeCard();

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
            CreateCard();
        }
    }

    #region CardCreator

    void SetName()
    {
        dataCard.Name = EditorGUILayout.TextField("Card's Name :", dataCard.Name);

        foreach (DataCard item in allCards.dataCards)
        {
            if (item.Name == dataCard.Name)
            {
                EditorGUILayout.HelpBox("This Name's card already exist !", MessageType.Error);
                canCreate = false;
                return;
            }
        }
        canCreate = true;

    }

    void SetSprite()
    {
        sprite = (Sprite)EditorGUILayout.ObjectField(sprite, typeof(Sprite), false);

        if (sprite)
            dataCard.NameSprite = sprite.name;
        
    }


    void SetTypeCard()
    {
        dataCard.CardType = (ECardType)EditorGUILayout.EnumPopup("Card's Type : ", dataCard.CardType);

    }

    void SetRarity()
    {
        dataCard.Rarity = (ERarity)EditorGUILayout.EnumPopup("Card's Rarity : ", dataCard.Rarity);
    }


    void StatsManager()
    {
        for (int i = 0; i < dataCard.ListStats.statCards.Count; i++)
        {
            StatCard _currentStat = dataCard.ListStats.statCards[i];

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical();

            _currentStat.Stat = (ECardStat)EditorGUILayout.EnumPopup("Stat's Type : ", _currentStat.Stat);

            _currentStat.Data = EditorGUILayout.FloatField("Stat's Data : ", _currentStat.Data);

            EditorGUILayout.EndVertical();

            dataCard.ListStats.statCards[i] = _currentStat;

            if (GUILayout.Button(" X "))
                dataCard.RemoveStat(_currentStat);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
        }
    }

    void AddStat()
    {
        newStat.Stat = (ECardStat)EditorGUILayout.EnumPopup("Stat's Type : ", newStat.Stat);

        newStat.Data = EditorGUILayout.FloatField("Stat's Data : ", newStat.Data);

        if (GUILayout.Button(" Add Stat "))
            dataCard.AddStat(newStat);
    }

    #endregion

    void CreateCard()
    {
        if (!canCreate) return;

        Debug.Log("CreateCard");

        allCards.dataCards.Add(dataCard);

        JsonUnitility.WriteOnJson(HAD_PathHelper.PathToCardsDataBase, HAD_PathHelper.PathToDatabase, allCards);

        AssetDatabase.Refresh();

        ResetTool();
    }



}
