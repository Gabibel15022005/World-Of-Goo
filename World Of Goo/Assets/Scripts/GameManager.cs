using System;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static Action AddToMove;
    public static Action AddCollectableAction;

    public static Action<Goo> AddGooToGameManager;
    public static Action<Goo> RemoveGooToGameManager;
    private List<Goo> goos = new List<Goo>();

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
    private bool isLevelFinished = false;
    private bool isVictoryPanelSpawned = false;

    void Update()
    {
        CheckOnGoo();

        if (goos.Count == 0 && isLevelFinished && !isVictoryPanelSpawned)
        {
            isVictoryPanelSpawned = true;
            SpawnVictoryPanel();
        }

        if (!isTimerPaused)
        {
            UpdateTimer();
            UpdateTimerUI();
        }
    }
    void SpawnVictoryPanel()
    {
        VictoryPanel.SpawnVictoryPanelAction?.Invoke(levelStats);
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
        isLevelFinished = true;
        PauseTimer(true);
    }
    void AddToMoveCount() // ajoute au nombre de coup jouer en tout
    {
        moveCount++;
        levelStats.maxMoveNb = moveCount;
        UpdateMoveCountUIAction?.Invoke(moveCount);
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
