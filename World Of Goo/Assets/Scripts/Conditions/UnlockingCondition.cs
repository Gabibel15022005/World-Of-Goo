using UnityEngine;

[System.Serializable]
public class ConditionEntry
{
    public UnlockingCondition condition; // la logique (ex: TimeCondition)
    public int intValue;                 // ex: maxTime ou requiredKills
}

public abstract class UnlockingCondition : ScriptableObject
{
    public abstract bool CheckWithOverride(LevelStats stats, int value);
    public abstract string GenerateDefaultDescription(int value);
}