using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action AddToMove;
    public static Action StartTheGame;
    public static Action StartTheTimer;

    public static Action<Goo> AddGooToGameManager;
    public static Action<Goo> RemoveGooToGameManager;
    private List<Goo> goos = new List<Goo>();
    private bool hasGameStarted = false;


    public static Action<int> UpdateTimerUI;
    public static Action<int> UpdateMoveCountUI;
    private int moveCount = 0;


    void Start()
    {
        
    }

    void Update()
    {
        //if (!hasGameStarted) return;
        
        CheckOnGoo();

    }

    void CheckOnGoo()
    {
        foreach (Goo goo in goos)
        {
            if (goo == null)
            {
                goos.Remove(goo);
                CheckOnGoo();
                break;
            }
        }
    }

    void EndTheLevel(Transform transform) // déclenche la fin du niveau
    {
        //  start a little timer before spawning or activating the UI for the end of the level

    }

    void AddToMoveCount() // ajoute au nombre de coup jouer en tout
    {
        moveCount++;
        UpdateMoveCountUI?.Invoke(moveCount);
    }

    void StartGame() // appelle l'event ou commencer le jeu
    {
        StartTheGame?.Invoke();
    }
    void StartTimer() // appelle l'event pour commencer le timer 
    {
        StartTheTimer?.Invoke();
    }

    void AddGooToGameManagerF(Goo goo)
    {
        goos.Add(goo);
    }

    void OnEnable()
    {
        EndOfLevel.PosEndOfLevel += EndTheLevel;
        AddGooToGameManager += AddGooToGameManagerF;
        AddToMove += AddToMoveCount;
    }
    void OnDisable()
    {
        EndOfLevel.PosEndOfLevel -= EndTheLevel;
        AddGooToGameManager -= AddGooToGameManagerF;
        AddToMove -= AddToMoveCount;
    }
}
