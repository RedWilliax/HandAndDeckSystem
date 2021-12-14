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
        HAD_InputManager.OnLMBClick -= GrabCard;
        HAD_InputManager.OnLMBClick -= LayingCard;
        HAD_InputManager.OnRMBClick -= UnGrabCard;
    }

    void GrabCard(bool _hold)
    {
        if (!HAD_MousePointer.Instance.InfoImpact.collider) return;

        if (!_hold) return;

        if (!currentCard)
        {
            currentCard = HAD_MousePointer.Instance.InfoImpact.collider.GetComponent<HAD_Card>();
            mover.Hand.RemoveCard(currentCard);
        }

        if (currentCard && currentCard.Owner != mover)
            currentCard = null;
    }

    void UnGrabCard(bool _hold)
    {
        if (_hold && currentCard)
        {
            mover.Hand.AddCard(currentCard);
            currentCard.SetPositon(currentCard.Anchor);
            currentCard = null;
        }
    }

    void LayingCard(bool _hold)
    {
        if (!_hold || !currentCard) return;

        if(currentCard.AboveABoard(out HAD_Board _board))
        {
            if (_board.IsFull) return;

            //ajout de la possibilité de posé sur le board adverse 

            _board.AddCard(currentCard);
            currentCard = null;
        }
    }


}
