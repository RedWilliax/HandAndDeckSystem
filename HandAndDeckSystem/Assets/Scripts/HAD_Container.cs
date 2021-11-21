using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Container
{

    protected List<HAD_Card> cards = new List<HAD_Card>();

    [SerializeField, Range(1, 1000)]protected int maxCards;

    public HAD_Container(int maxCards)
    {
        this.maxCards = maxCards;
    }

    public int CardQuantity => cards.Count;
    public bool IsFull => CardQuantity >= maxCards;
    public bool IsEmpty => CardQuantity <= 0;

    public List<HAD_Card> Cards => cards;

    #region Handler

    public virtual HAD_Card GetCard()
    {
        return cards[Random.Range(0, CardQuantity)];
    }

    public virtual bool AddCard(HAD_Card _card)
    {
        if (CardQuantity < maxCards)
            cards.Add(_card);

        return CardQuantity < maxCards;
    }

    public virtual bool RemoveCard(HAD_Card _card)
    {
        if (CardQuantity > 0)
            cards.Remove(_card);

        return CardQuantity > 0;
    }

    


    #endregion





}
