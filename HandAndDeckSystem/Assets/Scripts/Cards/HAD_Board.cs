using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HAD_Board : MonoBehaviour
{
    public event Action OnLay = null;
    public event Action OnGrab = null;

    public event Action OnBoardFull = null;

    [SerializeField]
    HAD_Player owner;

    [SerializeField]
    int maxCardOnBoard = 0;

    [SerializeField, Range(0, 10)]
    float spacingCard = 0;

    HAD_Container boardCountainer;

    public HAD_Player Owner { get => owner; private set => owner = value; }

    public bool IsFull
    {
        get
        {
            OnBoardFull?.Invoke();
            return boardCountainer.IsFull;
        }
    }


    private void Awake()
    {
        if (!owner)
            Debug.Log("Missing Owner !");

        OnLay += SetPosAllCard;
        OnGrab += SetPosAllCard;
        HAD_InputManager.OnLMBUnClick += AssigneBoardOnCard;

        boardCountainer = new HAD_Container(maxCardOnBoard);

    }

    private void OnDestroy()
    {
        OnLay -= SetPosAllCard;
        OnGrab -= SetPosAllCard;

        HAD_InputManager.OnLMBUnClick -= AssigneBoardOnCard;
    }

    public void AddCard(HAD_Card _card)
    {
        boardCountainer.AddCard(_card);

        OnLay.Invoke();
    }

    public void RemoveCard(HAD_Card _card)
    {
        boardCountainer.RemoveCard(_card);

        OnGrab?.Invoke();
    }

    void SetPosAllCard()
    {
        for (int i = 0; i < boardCountainer.CardQuantity; i++)
        {
            int _indexPos = i - boardCountainer.CardQuantity;

            Vector3 _anchor = transform.position + (Vector3.right * (spacingCard * _indexPos) + (Vector3.up * 0.1f)) + new Vector3(boardCountainer.CardQuantity + 1, 0, 0);

            boardCountainer.Cards[i].Anchor = _anchor;
            boardCountainer.Cards[i].SetPositon(_anchor);
        }
    }

    void AssigneBoardOnCard(bool _unHold)
    {
        if (!_unHold) return;

        for (int i = 0; i < boardCountainer.CardQuantity; i++)
        {
            if (boardCountainer.Cards[i].ItsBoard) continue;

            boardCountainer.Cards[i].ItsBoard = this;
        }
    }


    private void OnGUI()
    {
        Vector3 _positionWorld = transform.forward * 3.5f;

        Vector3 _position = Camera.main.WorldToScreenPoint(_positionWorld);

        GUILayout.BeginArea(new Rect(_position.x - 50, _position.y - 25, 100, 200));

        GUILayout.Label($"Board : {boardCountainer.CardQuantity} / {boardCountainer.MaxCards}");

        if (boardCountainer.IsFull)
            GUILayout.Label($"Board is full !");
        else if (boardCountainer.IsEmpty)
            GUILayout.Label($"Board is Empty.");

        GUILayout.EndArea();

    }


    private void OnDrawGizmos()
    {
        //Gizmos.color = new Color(1, 0, 0, 0.2f);

        //Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);

        //for (int i = 0; i < boardcountainer.cardquantity; i++)
        //{
        //    gizmos.color = color.red;

        //    gizmos.drawsphere(boardcountainer.cards[i].anchor, (i + 0.1f) / 10.0f);

        //}

    }


}
