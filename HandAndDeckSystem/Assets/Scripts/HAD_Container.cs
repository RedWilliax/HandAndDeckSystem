using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Container : MonoBehaviour
{

    List<HAD_Card> cards = new List<HAD_Card>();

    [SerializeField, Range(1, 1000)] int maxCards = 10;

    public int CardQuantity => cards.Count;

    #region Handler

    public bool AddCard(HAD_Card _card)
    {
        if (CardQuantity <= maxCards)
            cards.Add(_card);

        return CardQuantity <= maxCards;
    }

    public bool RemoveCard(HAD_Card _card)
    {
        if (CardQuantity > 0)
            cards.Remove(_card);

        return CardQuantity > 0;
    }

    #endregion





}
