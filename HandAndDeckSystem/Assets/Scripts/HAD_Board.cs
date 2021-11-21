using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Board : MonoBehaviour
{
    public event Action OnLay = null;

    [SerializeField]
    int maxCardOnBoard;

    [SerializeField, Range(0, 10)]
    float spacingCard;


    HAD_Container boardCountainer;

    private void Awake()
    {
        OnLay += SetPosAllCard;

        boardCountainer = new HAD_Container(maxCardOnBoard);

    }

    public void AddCard(HAD_Card _card)
    {
        OnLay.Invoke();

        boardCountainer.AddCard(_card);
    }

    void SetPosAllCard()
    {

    }




}
