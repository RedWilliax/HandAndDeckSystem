using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HAD_TurnSystem
{
    public event Action OnBeginTurn = null;
    public event Action OnEndTurn = null;
    public event Action OnBeginGame = null;
    public event Action OnEndGame = null;

    int turnCounter;
    readonly int turnMax;
    readonly float timeTurn;
    float timeWaited;

    HAD_Player currentPlayer;

    readonly bool infinitTurn;

    public int TurnCount => turnCounter;
    public int TurnMax => turnMax;
    public float TimeTurn => timeTurn;
    public HAD_Player CurrentPlayer => currentPlayer;

    public bool InfinitTurn => infinitTurn;

    public HAD_TurnSystem(int turnCounter, int turnMax, float timeTurn, float timeWaited, bool infinitTurn = true)
    {
        this.turnCounter = turnCounter;
        this.turnMax = turnMax;
        this.infinitTurn = infinitTurn;
        this.timeTurn = timeTurn;
        this.timeWaited = timeWaited;
    }

    public void Enter()
    {
        OnBeginGame?.Invoke();
        OnEndTurn += SetCurrentPlayer;
    }

    public void Update()
    {
        if (timeWaited <= 0)
            OnBeginTurn?.Invoke();

        timeWaited += Time.deltaTime;

        if (timeWaited < timeTurn) return;

        turnCounter++;
        timeWaited = 0;

        OnEndTurn?.Invoke();

        if (!infinitTurn && turnCounter >= turnMax)
            Exit();

    }

    public void Exit()
    {
        OnEndGame?.Invoke();
    }

    public void EndTurn()
    {
        timeWaited = timeTurn;
    }

    void SetCurrentPlayer()
    {
        int count = HAD_GameManager.Instance.PlayerCount;
        for (int i = 0; i < count; i++)
        {
            if (HAD_GameManager.Instance.AllPlayer[i] == currentPlayer)
                currentPlayer = HAD_GameManager.Instance.AllPlayer[(i + 1) > count ? 0 : (i + 1)];
        }
    }


}
