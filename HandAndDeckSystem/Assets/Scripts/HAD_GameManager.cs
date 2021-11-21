using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_GameManager : HAD_Singleton<HAD_GameManager>
{
    [SerializeField, Range(2, 15)] float firstPlanHeight;
    [SerializeField, Range(1, 100)] int maxCardHand = 10;
    [SerializeField, Range(1, 100)] int maxCardDeck = 30;

    [SerializeField] public bool makePreMadeDeck = false;

    [SerializeField] string pathPreMadeDeck = "Card_Generic";
    HAD_Deck currentDeck = null;
    public int MaxCardHand => maxCardHand;
    public int MaxCardDeck => maxCardDeck;
    public float FirstPlanHeight => firstPlanHeight;

    public HAD_Deck CurrentDeck => currentDeck;

    private void Awake()
    {
        base.Awake();

        if (makePreMadeDeck)
            MakerPreMadeDeck();
        else
            BuildDeck();
    }

    void MakerPreMadeDeck()
    {
        currentDeck = new HAD_Deck(maxCardDeck);

        for (int i = 0; i < maxCardDeck; i++)
        {
            GameObject _ob = (GameObject)Resources.Load(pathPreMadeDeck);

            HAD_Card _card = Instantiate(_ob, new Vector3(0, 0.5f, 3), Quaternion.identity).GetComponent<HAD_Card>();

            currentDeck.AddCard(_card);
        }
    }

    void BuildDeck()
    {



    }


}
