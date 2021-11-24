using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering.HighDefinition;

public class HAD_Deck : HAD_Container
{
    public HAD_Deck(int maxCards) : base(maxCards)
    {
      
    }

    public void FillDeck(HAD_Deck _deck)
    {
        _deck.cards.ForEach(n => AddCard(n));
    }

    public void FillDeck(List<HAD_Card> _cards)
    {
        _cards.ForEach(n => AddCard(n));
    }

    public HAD_Card DrawCard()
    {
        HAD_Card _card = GetCard();

        RemoveCard(_card);

        return _card;
    }
}
