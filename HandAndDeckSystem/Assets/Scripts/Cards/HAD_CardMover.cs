using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HAD_CardMover : MonoBehaviour
{

    [SerializeField, Range(0, 1)]
    float lerpGrab = 1f;

    HAD_Player mover = null;

    HAD_Card currentCard = null;

    private void Awake()
    {
        HAD_InputManager.OnLMBClick += GrabCard;
        HAD_InputManager.OnLMBClick += LayingCard;
        HAD_InputManager.OnRMBClick += UnGrabCard;

        HAD_GameManager.Instance.SubEndTurn(UnGradCard);

        mover = gameObject?.GetComponent<HAD_Player>();

        if (!mover)
            Debug.LogError("Missing HAD_Player for your HAD_CardMover");

    }

    private void LateUpdate()
    {
        if (currentCard)
            currentCard.SetPositon(Vector3.Lerp(currentCard.transform.position, new Vector3(HAD_MousePointer.Instance.InfoImpact.point.x, HAD_GameManager.Instance.FirstPlanHeight, HAD_MousePointer.Instance.InfoImpact.point.z), lerpGrab));

    }

    private void OnDestroy()
    {
        UnsubscribeCard();
    }

    void UnsubscribeCard()
    {
        HAD_InputManager.OnLMBClick -= GrabCard;
        HAD_InputManager.OnLMBClick -= LayingCard;
        HAD_InputManager.OnRMBClick -= UnGrabCard;
        HAD_GameManager.Instance.UnSubEndTurn(UnGradCard);
    }


    void GrabCard(bool _hold)
    {
        if (!HAD_MousePointer.Instance.InfoImpact.collider || !HAD_GameManager.Instance.IsMineTurn(mover)) return;

        if (!_hold) return;

        if (!currentCard)
            currentCard = HAD_MousePointer.Instance.InfoImpact.collider.GetComponent<HAD_Card>();

        if (currentCard && (currentCard.Owner != mover || currentCard.ItsBoard))
        {
            currentCard = null;
            return;
        }

        mover.Hand.RemoveCard(currentCard);
    }

    void UnGrabCard(bool _hold)
    {
        if (!HAD_GameManager.Instance.IsMineTurn(mover)) return;

        if (_hold && currentCard)
        {
            mover.Hand.AddCard(currentCard);
            currentCard.SetPositon(currentCard.Anchor);
            currentCard = null;
        }
    }

    void UnGradCard()
    {
        UnGrabCard(true);
    }

    void LayingCard(bool _hold)
    {
        if (!_hold || !HAD_GameManager.Instance.IsMineTurn(mover) || !currentCard) return;

        switch (currentCard.DataCard.CardType)
        {
            case ECardType.NONE:
                break;
            case ECardType.Unite:
                LayingUnite();
                break;
            case ECardType.Spell:
                LayingSpell();
                break;
            case ECardType.Stuff:
                LayingStuff();
                break;
        }

    }

    void LayingUnite()
    {
        if (currentCard.AboveABoard(out HAD_Board _board))
        {
            if (_board.IsFull) return;

            _board.AddCard(currentCard);

            currentCard = null;
        }
    }

    void LayingSpell()
    {
        if (currentCard.AboveACard(out HAD_Card _card))
        {

        }
    }

    void LayingStuff()
    {

        if (currentCard.AboveACard(out HAD_Card _card))
        {
            if (_card.DataCard.CardType != ECardType.Unite) return;



        }

    }


}
