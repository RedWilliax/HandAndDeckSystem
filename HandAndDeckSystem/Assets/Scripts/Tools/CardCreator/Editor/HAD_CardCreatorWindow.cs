using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum CardType
{
    NONE,
    SPELL,
    STUFF,
    UNITE

}

public class HAD_CardCreatorWindow : EditorWindow
{
    CardType cardType = CardType.NONE;


    [MenuItem("Tools/Card/CardCreator")]
    static void Init()
    {
        HAD_CardCreatorWindow window = (HAD_CardCreatorWindow)GetWindow(typeof(HAD_CardCreatorWindow));

        window.Show();

    }
    private void OnGUI()
    {
        cardType = (CardType)EditorGUILayout.EnumPopup("Type of Card", cardType);

        EditorGUILayout.Space();



        HAD_Card _cardtoCreate = null;

        switch (cardType)
        {
            case CardType.NONE:
                break;
            case CardType.SPELL:
                _cardtoCreate = new HAD_Spell();
                ParameterCard(ref _cardtoCreate);
                break;
            case CardType.STUFF:
                _cardtoCreate = new HAD_Stuff();
                ParameterCard(ref _cardtoCreate);
                break;
            case CardType.UNITE:
                _cardtoCreate = new HAD_Unite();
                ParameterUnite(ref _cardtoCreate);
                break;
        }



        if (GUILayout.Button("Create Card"))
        {
            CreateCard(_cardtoCreate);
        }
    }

    #region CardCreator

    void ParameterCard(ref HAD_Card _cardToCreate)
    {
        string _name = "Default_Name";
        int _cost = 0;
        ERarity _rarity = ERarity.COMMON;
        SpriteRenderer _sprite = null;

        name = EditorGUILayout.TextField("Name :", name);
        _cost = EditorGUILayout.IntSlider("Cost ", _cost, 0, 10);
        _rarity = (ERarity)EditorGUILayout.EnumPopup("Rarity ", _rarity);
        _sprite = (SpriteRenderer)EditorGUILayout.ObjectField(_sprite, typeof(SpriteRenderer), false);

        _cardToCreate.Name = _name;
        _cardToCreate.Cost = _cost;
        _cardToCreate.Rarity = _rarity;
        _cardToCreate.Sprite = _sprite;

    }

    void ParameterUnite(ref HAD_Card _cardToCreate)
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
