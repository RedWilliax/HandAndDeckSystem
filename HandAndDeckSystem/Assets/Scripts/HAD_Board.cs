using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Board : MonoBehaviour
{
    public event Action OnLay = null;

    public event Action OnBoardFull = null;

    [SerializeField]
    HAD_Player owner;

    [SerializeField]
    int maxCardOnBoard = 0;

    [SerializeField, Range(0, 10)]
    float spacingCard = 0;

    HAD_Container boardCountainer;

    HAD_Card selectedCard = null;

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
        HAD_InputManager.OnLMBClick += SelectCard;
        HAD_InputManager.OnRMBClick += ResetSelectedCard;


        boardCountainer = new HAD_Container(maxCardOnBoard);

    }

    public void AddCard(HAD_Card _card)
    {
        boardCountainer.AddCard(_card);

        _card.ItsBoard = this;

        OnLay.Invoke();

    }

    void SetPosAllCard()
    {
        for (int i = 0; i < boardCountainer.CardQuantity; i++)
        {
            int _indexPos = ((i % 2) > 0 ? -(i + 1) / 2 : (i + 1) / 2);

            Vector3 _anchor = transform.position + (-Vector3.right * (spacingCard * _indexPos) + (Vector3.up * 0.1f));

            boardCountainer.Cards[i].Anchor = _anchor;
            boardCountainer.Cards[i].SetPositon(_anchor);
        }
    }

    void SelectCard(bool _click)
    {
        if (!_click || selectedCard) return;

        if (Physics.Raycast(HAD_MousePointer.Instance.InfoImpact.point, -Vector3.up, out RaycastHit raycastHit, 10))
        {
            HAD_Card _ob = raycastHit.collider.gameObject.GetComponent<HAD_Card>();

            if (!_ob || !_ob.ItsBoard) return;

            selectedCard = _ob;
        }
    }

    void ResetSelectedCard(bool _click)
    {

        if (!_click || !selectedCard) return;

        selectedCard = null;

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);

        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);

        Gizmos.color = new Color(0, 1, 0, 0.5f);

        if (boardCountainer != null)
            foreach (HAD_Card item in boardCountainer.Cards)
                Gizmos.DrawCube(item.Anchor, item.GetComponent<BoxCollider>().bounds.size);

        Gizmos.color = new Color(1, 0, 1, 0.5f);

        if (selectedCard)
            Gizmos.DrawCube(selectedCard.Anchor, selectedCard.GetComponent<BoxCollider>().bounds.size);

    }


}
