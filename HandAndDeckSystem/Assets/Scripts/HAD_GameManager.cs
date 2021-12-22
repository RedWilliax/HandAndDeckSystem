using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_GameManager : HAD_Singleton<HAD_GameManager>
{
    [SerializeField, Range(2, 15)] float firstPlanHeight;

    [SerializeField] HAD_Player[] allPlayer;

    [SerializeField] string pathPreMadeDeck = "Card_Generic";

    public float FirstPlanHeight => firstPlanHeight;

    public HAD_Player[] AllPlayer => allPlayer;

    public int PlayerCount => allPlayer.Length;

    private void Awake()
    {
        base.Awake();
    }


/// <summary>
/// A modifier, 
/// </summary>
/// <param name="_maxCardDeck"></param>
/// <returns></returns>
    public HAD_Deck MakerPreMadeDeck(int _maxCardDeck, Vector3 _deckPosition)
    {
        HAD_Deck _currentDeck = new HAD_Deck(_maxCardDeck);

        for (int i = 0; i < _maxCardDeck; i++)
        {
            GameObject _ob = (GameObject)Resources.Load(pathPreMadeDeck);

            HAD_Card _card = Instantiate(_ob, _deckPosition, Quaternion.identity).GetComponent<HAD_Card>();

            _currentDeck.AddCard(_card);
        }

        return _currentDeck;
    }

    void BuildDeck()
    {



    }


}
