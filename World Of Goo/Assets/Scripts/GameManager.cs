using System;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static Action AddToMove;
    public static Action StartTheGameAction;
    public static Action PauseTimerAction;
    public static Action AddCollectableAction;

    public static Action<Goo> AddGooToGameManager;
    public static Action<Goo> RemoveGooToGameManager;
    private List<Goo> goos = new List<Goo>();
    private bool hasGameStarted = false;

    private float timer = 0;
    private int numberOfSecond = 0;
    private int lastSecond = -1;
    private int minutes = 0;
    private int seconds = 0;
    private bool isTimerPaused = false;

    private LevelStats levelStats = new LevelStats();
    public static Action<int, int> UpdateTimerUIAction;
    public static Action<int> UpdateMoveCountUIAction;
    private int moveCount = 0;

    void Update()
    {
        //if (!hasGameStarted) return;

        CheckOnGoo();

        if (!isTimerPaused)
        {
            UpdateTimer();
            UpdateTimerUI();
        }
    }
    void UpdateTimer()
    {
        timer += Time.deltaTime;

        numberOfSecond = Mathf.FloorToInt(timer);
        levelStats.timeTaken = numberOfSecond;

        minutes = Mathf.FloorToInt(timer / 60f);
        seconds = Mathf.FloorToInt(timer % 60f);
    }
    void UpdateTimerUI()
    {
        if (seconds != lastSecond)
        {
            lastSecond = seconds;
            UpdateTimerUIAction?.Invoke(minutes, seconds);
        }
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
        VictoryPanel.SpawnVictoryPanelAction?.Invoke(levelStats);
        PauseTimer(true);
    }
    void AddToMoveCount() // ajoute au nombre de coup jouer en tout
    {
        moveCount++;
        levelStats.maxMoveNb = moveCount;
        UpdateMoveCountUIAction?.Invoke(moveCount);
    }
    void StartGame() // appelle l'event ou commencer le jeu
    {
        StartTheGameAction?.Invoke();
    }
    void PauseTimer(bool value) // appelle l'event pour commencer le timer 
    {
        isTimerPaused = value;
    }
    void AddGooToGameManagerF(Goo goo)
    {
        goos.Add(goo);
    }
    void AddCollectable()
    {
        levelStats.collectableFound++;
    }
    void OnEnable()
    {
        EndOfLevel.PosEndOfLevel += EndTheLevel;
        AddCollectableAction += AddCollectable;
        AddGooToGameManager += AddGooToGameManagerF;
        AddToMove += AddToMoveCount;
    }
    void OnDisable()
    {
        EndOfLevel.PosEndOfLevel -= EndTheLevel;
        AddCollectableAction -= AddCollectable;
        AddGooToGameManager -= AddGooToGameManagerF;
        AddToMove -= AddToMoveCount;
    }
}
