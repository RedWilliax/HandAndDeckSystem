using JsonCom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_GameManager : HAD_Singleton<HAD_GameManager>
{
    [SerializeField, Range(2, 15)] float firstPlanHeight;

    [SerializeField] HAD_Player[] allPlayer;

    [SerializeField] string pathPreMadeDeck = "Card_Generic";

    [SerializeField] Vector3 positionEndButton;

    public float FirstPlanHeight => firstPlanHeight;

    public HAD_Player[] AllPlayer => allPlayer;

    public int PlayerCount => allPlayer.Length;

    public HAD_Player CurrentPlayer => turnSystem.CurrentPlayer;

    HAD_TurnSystem turnSystem;


    private void Awake()
    {
        base.Awake();

        turnSystem = new HAD_TurnSystem();

    }

    private void Start()
    {
        turnSystem.Enter();

    }

    private void Update()
    {

        turnSystem.Update();

    }


    public bool IsMineTurn(HAD_Player _player)
    {
        return CurrentPlayer == _player;
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

            //Temp
            ListCardData allCards = new ListCardData();
            JsonUnitility.ReadJson(ref allCards, HAD_PathHelper.PathToCardsDataBase, HAD_PathHelper.PathToDatabase);
            _card.DataCard = allCards.dataCards[0];
            _card.InitializeCard();
            //

            _currentDeck.AddCard(_card);
        }

        return _currentDeck;
    }

    void BuildDeck()
    {



    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;

        Gizmos.DrawSphere(positionEndButton, 0.1f);

    }

    private void OnGUI()
    {
        Vector3 _position = Camera.main.WorldToScreenPoint(positionEndButton);

        GUILayout.BeginArea(new Rect(_position.x -50, _position.y -50, 100, 200));

        GUILayout.Label($"Time : {turnSystem.TimeWaited: 0.0}");
        GUILayout.Label($"Turn : {turnSystem.TurnCount}");
        GUILayout.Label($"Current : {turnSystem.CurrentPlayer.name}");

        if(GUILayout.Button("End Turn"))
        {
            turnSystem.EndTurn();
        }
        
        GUILayout.EndArea();

    }


}
