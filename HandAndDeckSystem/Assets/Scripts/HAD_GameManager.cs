using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_GameManager : MonoBehaviour
{
    static HAD_GameManager instance = null;

    public static HAD_GameManager Instance => instance;

    [SerializeField, Range(1, 100)] int maxCardHand = 10;
    [SerializeField, Range(1, 100)] int maxCardDeck = 30;

    [SerializeField] public bool makePreMadeDeck = false;

    [SerializeField] string pathPreMadeDeck = "Card_Generic";
    HAD_Deck currentDeck = null;
    public int MaxCardHand => maxCardHand;
    public int MaxCardDeck => maxCardDeck;

    public HAD_Deck CurrentDeck => currentDeck;

    private void Awake()
    {
        if (!instance) instance = this;



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
