using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Container : MonoBehaviour
{

    List<HAD_Card> cards = new List<HAD_Card>();

    [SerializeField] int maxCards = 10;

    #region Handler

    public void AddCard(HAD_Card _card)
    {
        cards.Add(_card);
    }

    public void RemoveCard(HAD_Card _card)
    {
        cards.Remove(_card);
    }

    #endregion




}
