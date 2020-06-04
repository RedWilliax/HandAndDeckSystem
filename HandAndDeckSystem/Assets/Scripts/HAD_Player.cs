using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HAD_Player : MonoBehaviour
{
    [SerializeField, Range(0, 180)] float angleHand = 90;

    [SerializeField, Range(0, 10)] float distHandCard = 1;

    HAD_Hand hand = null;

    HAD_Deck deck = null;

    private void Start()
    {
        hand = new HAD_Hand(HAD_GameManager.Instance.MaxCardHand);

        deck = new HAD_Deck(HAD_GameManager.Instance.MaxCardDeck);

        deck.FillDeck(HAD_GameManager.Instance.PreMadeDeck);

        hand.OnDrawCard += SetPosHandCard;

    }

    void DrawCard()
    {
        if (hand.IsFull) return;

        hand.AddCard(deck.DrawCard());
    }

    void SetPosHandCard()
    {
        float _angle = angleHand / hand.CardQuantity;

        float _angleBetweenEachCard = _angle * 2;

        for (int i = 0; i < hand.CardQuantity; i++)
        {
            float _x = Mathf.Cos((_angle + _angleBetweenEachCard * i) * Mathf.Deg2Rad) * distHandCard;
            float _z = Mathf.Sin((_angle + _angleBetweenEachCard * i) * Mathf.Deg2Rad) * distHandCard;

            hand.Cards[i].SetPositon(new Vector3(transform.position.x, transform.position.y + i / 10.0f + 0.1f, transform.position.z) + new Vector3(_x * 1.3f, 0, _z * 0.6f + distHandCard));

            hand.Cards[i].LookAt(new  Vector3(transform.position.x + (_x * 0.9f), transform.position.y, transform.position.z));
        }
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(5, 5, 100, 200));

        GUILayout.Label($"Deck : {deck.CardQuantity} / {HAD_GameManager.Instance.MaxCardDeck}");

        GUILayout.Label($"Hand : {hand.CardQuantity} / {HAD_GameManager.Instance.MaxCardHand}");

        if (hand.IsFull)
            GUILayout.Label($"Hand is full !");
        else
        {
            if (GUILayout.Button("DrawCard"))
                DrawCard();
        }

        GUILayout.EndArea();

    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.2f);

    }

}
