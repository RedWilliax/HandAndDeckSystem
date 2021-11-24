using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_Board : MonoBehaviour
{
    public event Action OnLay = null;

    public event Action OnBoardFull = null;

    [SerializeField]
    int maxCardOnBoard;

    [SerializeField, Range(0, 10)]
    float spacingCard;

    HAD_Container boardCountainer;

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
        OnLay += SetPosAllCard;

        boardCountainer = new HAD_Container(maxCardOnBoard);

    }

    public void AddCard(HAD_Card _card)
    {
        boardCountainer.AddCard(_card);

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

    private void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, 0, 0, 0.2f);

        Gizmos.DrawCube(transform.position, GetComponent<BoxCollider>().bounds.size);

    }


}
