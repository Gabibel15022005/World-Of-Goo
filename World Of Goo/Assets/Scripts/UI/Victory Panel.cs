using System;
using UnityEngine;

public class VictoryPanel : MonoBehaviour
{
    [SerializeField] Animator animator;
    public static Action SpawnVictoryPanelAction;

    void SpawnVictoryPanel()
    {
        if (animator == null)
        {
            Debug.Log($"There is no animator referenced in : {gameObject.name}");
            return;
        }

        animator.Play("SpawnVictoryPanel");
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
