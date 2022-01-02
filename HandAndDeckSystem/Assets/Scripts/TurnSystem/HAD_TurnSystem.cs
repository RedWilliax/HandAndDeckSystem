using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct TurnSystemData
{
    int turnMax;
    float timeTurn;
    bool infiniteTurn;
    bool infinitTimeTurn;
}

public class HAD_TurnSystem
{
    public event Action OnBeginTurn = null;
    public event Action OnEndTurn = null;
    public event Action OnBeginGame = null;
    public event Action OnEndGame = null;

    int turnCounter = 0;
    readonly int turnMax;
    readonly float timeTurn;
    float timeWaited = 0;

    HAD_Player currentPlayer;

    readonly bool infinitTurn;
    readonly bool infinitTimeTurn;

    public int TurnCount => turnCounter;
    public int TurnMax => turnMax;
    public float TimeTurn => timeTurn;
    public float TimeWaited => timeWaited;
    public HAD_Player CurrentPlayer => currentPlayer;

    public bool InfinitTurn => infinitTurn;
    public bool InfinitTimeTurn => infinitTimeTurn;

    public HAD_TurnSystem(bool infinitTurn = false, bool infinitTimeTurn = false,  float timeTurn = 10, int turnMax = 10)
    {
        this.turnMax = turnMax;
        this.timeTurn = timeTurn;
        timeWaited = timeTurn;
        this.infinitTurn = infinitTurn;
        this.infinitTimeTurn = infinitTimeTurn;
    }

    public void Enter()
    {
        OnBeginGame?.Invoke();
        OnEndTurn += ChangeCurrentPlayer;

        if (!currentPlayer)
            SetCurrentPlayer(HAD_GameManager.Instance.AllPlayer[UnityEngine.Random.Range(0, HAD_GameManager.Instance.PlayerCount)]);
    }

    public void Update()
    {
        if (timeWaited <= 0)
            OnBeginTurn?.Invoke();

        if (!InfinitTimeTurn)
            timeWaited -= Time.deltaTime;

        if (timeWaited > 0) return;

        EndTurn();
    }

    public void Exit()
    {
        OnEndGame?.Invoke();
    }

    public void EndTurn()
    {
        timeWaited = timeTurn;

        if (!InfinitTurn)
            turnCounter++;

        OnEndTurn?.Invoke();
    }

    void ChangeCurrentPlayer()
    {
        int count = HAD_GameManager.Instance.PlayerCount;
        for (int i = 0; i < count; i++)
        {
            if (HAD_GameManager.Instance.AllPlayer[i] == currentPlayer)
            {
                SetCurrentPlayer(HAD_GameManager.Instance.AllPlayer[(i + 1) >= count ? 0 : (i + 1)]);
                return;
            }
        }
    }

    void SetCurrentPlayer(HAD_Player _player)
    {
        currentPlayer = _player;
    }

}
