﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HAD_Player : MonoBehaviour
{
    [SerializeField, Range(-10, 10)] float distHandCard = 0;
    [SerializeField, Range(-10, 10)] float cardShift = 1;
    [SerializeField, Range(0, 1)] float cardStackHeight = 0.1f;

    [SerializeField, Range(1, 100)] int maxDeckCard = 30;
    [SerializeField, Range(1, 100)] int maxHandCard = 30;

    [SerializeField] Vector3 positionDeck = new Vector3(5, 0, 0);
    [SerializeField] Vector3 positionDiscardPile = new Vector3(5, 0, 0);

    [Tooltip("This variable it's use when we select a card on a board")]
    [SerializeField] LayerMask layer = 0;

    #region Debug

    [SerializeField] Vector2 positionDebuger;

    #endregion

    Vector3 anchorDeck => gameObject.transform.position + positionDeck;
    Vector3 anchorDiscardPile => gameObject.transform.position + positionDiscardPile;

    HAD_Hand hand = null;

    HAD_Deck deck = null;

    HAD_Deck discardPile = null;

    HAD_Card selectedCard = null;

    public HAD_Hand Hand => hand;
    public HAD_Deck Deck => deck;
    public HAD_Deck DiscardPile => discardPile;
    public LayerMask Layer => layer;
    public HAD_Card SelectedCard => selectedCard;


    private void Awake()
    {
        HAD_InputManager.OnLMBClick += SelectCard;
        HAD_InputManager.OnLMBClick += ApplyDataOnCard;
        HAD_InputManager.OnRMBClick += UnSelectCard;
        HAD_GameManager.Instance.SubEndTurn(UnSelectedCard);

    }

    private void Start()
    {
        hand = new HAD_Hand(maxHandCard);

        deck = new HAD_Deck(maxDeckCard);

        discardPile = new HAD_Deck(maxDeckCard);

        hand.OnDrawCard += SetPosHandCard;
        hand.OnTakeCard += SetPosHandCard;

        if (HAD_GameManager.Instance)
            deck.FillDeck(HAD_GameManager.Instance.MakerPreMadeDeck(maxDeckCard, anchorDeck));
    }

    private void OnDestroy()
    {
        HAD_InputManager.OnLMBClick -= SelectCard;
        HAD_InputManager.OnLMBClick -= ApplyDataOnCard;
        HAD_InputManager.OnRMBClick -= UnSelectCard;
        if (HAD_GameManager.Instance)
            HAD_GameManager.Instance.UnSubEndTurn(UnSelectedCard);


    }

    void DrawCard()
    {
        if (hand.IsFull) return;

        HAD_Card _cardToHad = deck.DrawCard();

        _cardToHad.Owner = this;

        hand.AddCard(_cardToHad);
    }

    public void DiscardCard(HAD_Card _card)
    {
        if (!_card) return;

        _card.SetPositon(anchorDiscardPile);
        _card.Owner.DiscardPile.AddCard(_card);
        _card.Discard = true;

        if (_card.ItsBoard)
        {
            _card.ItsBoard.RemoveCard(_card);
            return;
        }

        if (_card.Owner.Hand.ExistCard(_card))
        {
            _card.Owner.Hand.RemoveCard(_card);
            return;
        }

        if (_card.Owner.Deck.ExistCard(_card))
            _card.Owner.Deck.RemoveCard(_card);



    }

    void SetPosHandCard()
    {
        for (int i = 0; i < hand.CardQuantity; i++)
        {
            Vector3 _anchor = new Vector3(transform.position.x + i + distHandCard, (i + 1) * cardStackHeight, transform.position.z + cardShift);

            hand.Cards[i].Anchor = _anchor;
            hand.Cards[i].SetPositon(_anchor);
        }

    }

    void SelectCard(bool _click)
    {
        if (selectedCard || !HAD_GameManager.Instance.IsMineTurn(this) || !_click) return;

        selectedCard = GetCard();

        if (!selectedCard) return;

        if (!selectedCard.ItsBoard || selectedCard.Owner != this)
            selectedCard = null;
    }

    void UnSelectCard(bool _click)
    {
        if (!HAD_GameManager.Instance.IsMineTurn(this) || !_click) return;

        UnSelectedCard();
    }

    //Création de cette méthode pour les delegates
    void UnSelectedCard()
    {
        selectedCard = null;
    }

    HAD_Card GetCard()
    {
        if (Physics.Raycast(HAD_MousePointer.Instance.InfoImpact.point, -Vector3.up, out RaycastHit _raycastInfo, 10, layer))
        {
            HAD_Card _card = _raycastInfo.collider.GetComponent<HAD_Card>();

            // Add limitation : warm up des carte, silence, etc

            return _card;
        }
        return null;

    }

    void ApplyDataOnCard(bool _hold)
    {
        if (!_hold || !selectedCard) return;

        HAD_Card _target = GetCard();

        if (!_target || _target == selectedCard || _target.Owner == this || !_target.ItsBoard || _target.DataCard.CardType != ECardType.Unite) return;

        selectedCard.GetStat(ECardStat.Atck, out float _atckSelected);

        _target.GetStat(ECardStat.Def, out float _defTarget);

        float _result = _defTarget - _atckSelected;

        if (_result < 0)
            _target.SetStat(ECardStat.Life, -_result);

        _target.SetStat(ECardStat.Def, _atckSelected);

    }

    private void OnGUI()
    {
        Vector3 _position = Camera.main.WorldToScreenPoint(new Vector3(positionDebuger.x, 0, -positionDebuger.y));

        GUILayout.BeginArea(new Rect(_position.x - 50, _position.y - 50, 100, 200));

        GUILayout.Label($"Deck : {deck.CardQuantity} / {deck.MaxCards}");
        GUILayout.Label($"Discrad : {discardPile.CardQuantity} / {discardPile.MaxCards}");
        GUILayout.Label($"Hand : {hand.CardQuantity} / {hand.MaxCards}");

        if (!hand.IsFull && GUILayout.Button("DrawCard"))
            DrawCard();

        if (hand.IsFull)
            GUILayout.Label($"Hand is full !");
        else if (hand.IsEmpty)
            GUILayout.Label($"Hand is Empty.");



        GUILayout.EndArea();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.color = Color.green;

        Gizmos.DrawSphere(anchorDeck, 0.2f);

        Gizmos.color = Color.magenta;

        Gizmos.DrawSphere(anchorDiscardPile, 0.2f);

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(new Vector3(positionDebuger.x, 0.5f, positionDebuger.y), 0.05f);

        if (!selectedCard) return;

        Gizmos.DrawLine(selectedCard.transform.position, HAD_MousePointer.Instance.MousePosition);

    }

}
