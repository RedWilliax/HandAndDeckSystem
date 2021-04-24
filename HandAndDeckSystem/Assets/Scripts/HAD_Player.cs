using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HAD_Player : MonoBehaviour
{
    [SerializeField, Range(-10, 10)] float distHandCard = 0;
    [SerializeField, Range(0, 10)] float cardShift = 1;
    [SerializeField, Range(0, 1)] float cardStackHeight = 0.1f;

    HAD_Hand hand = null;

    HAD_Deck deck = null;

    private void Start()
    {
        hand = new HAD_Hand(HAD_GameManager.Instance.MaxCardHand);

        deck = new HAD_Deck(HAD_GameManager.Instance.MaxCardDeck);

        hand.OnDrawCard += SetPosHandCard;

        if (HAD_GameManager.Instance.makePreMadeDeck)
            deck.FillDeck(HAD_GameManager.Instance.CurrentDeck);
    }

    void DrawCard()
    {
        if (hand.IsFull) return;

        hand.AddCard(deck.DrawCard());
    }

    void SetPosHandCard()
    {
        for (int i = 0; i < hand.CardQuantity; i++)
            hand.Cards[i].SetPositon(new Vector3(transform.position.x + i + distHandCard, (i + 1) * cardStackHeight, 0) + transform.position);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(5, 5, 100, 200));

        GUILayout.Label($"Deck : {deck.CardQuantity} / {HAD_GameManager.Instance.MaxCardDeck}");

        GUILayout.Label($"Hand : {hand.CardQuantity} / {HAD_GameManager.Instance.MaxCardHand}");

        if (hand.IsFull)
            GUILayout.Label($"Hand is full !");
        else if (hand.IsEmpty)
            GUILayout.Label($"Hand is Empty.");
        else
        {
            if (GUILayout.Button("DrawCard"))
                DrawCard();
        }

        if (GUILayout.Button("O/C Inventory"))
        {
            Debug.Log("O/C Inventory");

        }

        GUILayout.EndArea();

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f);

    }

}
