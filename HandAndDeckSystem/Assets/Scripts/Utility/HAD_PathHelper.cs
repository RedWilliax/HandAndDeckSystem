using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using JsonCom;


[Serializable]
public struct ListCardData
{
    [SerializeField] public List<DataCard> dataCards;

    public ListCardData(List<DataCard> dataCards)
    {
        this.dataCards = dataCards;
    }
}

public static class HAD_PathHelper
{
    public const string CardsDataBase = "CardsDataBase.txt";



    public static string PathToCardsDataBase => Path.Combine(PathToDatabase, CardsDataBase);

    public static string PathToDatabase => Path.Combine(Application.dataPath, "Data_Base");



    public static ListCardData GetCardsDataBase()
    {
        ListCardData _allCards = new ListCardData();

        JsonUnitility.ReadJson(ref _allCards, PathToCardsDataBase, PathToDatabase);

        return _allCards;
    }

}



