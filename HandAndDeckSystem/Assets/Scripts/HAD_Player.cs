using System;
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

    [Tooltip("This variable it's use when we select a card on a board")]
    [SerializeField] LayerMask Layer = 0;

    #region Debug

    [SerializeField] Vector2 positionDebuger;

    #endregion

    Vector3 anchorDeck => gameObject.transform.position + positionDeck;

    HAD_Hand hand = null;

    HAD_Deck deck = null;

    HAD_Card selectedCard = null;

    public HAD_Hand Hand => hand;

    private void Start()
    {
        HAD_InputManager.OnLMBClick += SelectCard;
        HAD_InputManager.OnRMBClick += UnSelectCard;
        HAD_GameManager.Instance.SubEndTurn(UnSelectedCard);

        hand = new HAD_Hand(maxHandCard);

        deck = new HAD_Deck(maxDeckCard);

        hand.OnDrawCard += SetPosHandCard;
        hand.OnTakeCard += SetPosHandCard;

        if (HAD_GameManager.Instance)
            deck.FillDeck(HAD_GameManager.Instance.MakerPreMadeDeck(maxDeckCard, anchorDeck));
    }

    private void OnDestroy()
    {
        HAD_InputManager.OnLMBClick -= SelectCard;
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
        if (!HAD_GameManager.Instance.IsMineTurn(this) || !_click) return;

        if (Physics.Raycast(HAD_MousePointer.Instance.InfoImpact.point, -Vector3.up, out RaycastHit _raycastInfo, 10, Layer))
        {
            HAD_Card _card = _raycastInfo.collider.GetComponent<HAD_Card>();

            if (!_card.ItsBoard || _card.Owner != this) return;

            selectedCard = _card;
        }

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


    private void OnGUI()
    {
        Vector3 _position = Camera.main.WorldToScreenPoint(new Vector3(positionDebuger.x, 0, -positionDebuger.y));

        GUILayout.BeginArea(new Rect(_position.x - 50, _position.y - 50, 100, 200));

        GUILayout.Label($"Deck : {deck.CardQuantity} / {deck.MaxCards}");

        GUILayout.Label($"Hand : {hand.CardQuantity} / {hand.MaxCards}");

        if (hand.IsFull)
            GUILayout.Label($"Hand is full !");
        else if (hand.IsEmpty)
            GUILayout.Label($"Hand is Empty.");

        if (!hand.IsFull && GUILayout.Button("DrawCard"))
            DrawCard();

        GUILayout.EndArea();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.color = Color.green;

        Gizmos.DrawSphere(anchorDeck, 0.2f);

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(new Vector3(positionDebuger.x, 0.5f, positionDebuger.y), 0.05f);

        if (!selectedCard) return;

        Gizmos.color = new Color(1, 0, 1, 0.2f);

        Gizmos.DrawCube(selectedCard.transform.position, selectedCard.GetComponent<BoxCollider>().bounds.size);
        Gizmos.DrawLine(selectedCard.transform.position, HAD_MousePointer.Instance.MousePosition);

    }

}
