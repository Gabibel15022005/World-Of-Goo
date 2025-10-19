using System;
using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] Animator animator;
    public static Action<LevelStats> SpawnVictoryPanelAction;
    [SerializeField] StarUI[] starsUI;
    [SerializeField] ConditionEntry[] conditions;
    [SerializeField] ButtonScript[] buttons;
    [HideInInspector] public LevelStats levelStats;

    [SerializeField] GoToScene nextLevelButton;
    public Level nextLevelScene;

    void Start()
    {
        nextLevelButton.scene = nextLevelScene.buildIndex;
    }

    void SpawnVictoryPanel(LevelStats stats)
    {
        levelStats = stats;

        SetEachStarDescription();

        if (animator == null)
        {
            Debug.Log($"There is no animator referenced in : {gameObject.name}");
            return;
        }

        animator.Play("SpawnVictoryPanel");

        PlayerPrefs.SetInt($"{nextLevelScene.sceneName} is unlocked", 1);
        PlayerPrefs.Save();
    }


    void UnlockEachStar()
    {
        StartCoroutine(UnlockEachStarCoroutine());
    }

    void SetEachStarDescription()
    {
        for (int i = 0; i < starsUI.Length; i++)
            starsUI[i].SetDescription(conditions[i].condition.GenerateDefaultDescription(conditions[i].intValue));
    }

    IEnumerator UnlockEachStarCoroutine()
    {
        for (int i = 0; i < starsUI.Length; i++)
        {
            if (starsUI[i].isAlreadyUnlocked || conditions[i].condition.CheckWithOverride(levelStats, conditions[i].intValue))
            {
                starsUI[i].UnlockStar();
                yield return new WaitForSeconds(0.5f);
            }
        }

        foreach (ButtonScript button in buttons)
            button.SpawnButton();
    }

    

    void OnEnable()
    {
        SpawnVictoryPanelAction += SpawnVictoryPanel;
    }
    void OnDisable()
    {
        SpawnVictoryPanelAction -= SpawnVictoryPanel;
    }

}
