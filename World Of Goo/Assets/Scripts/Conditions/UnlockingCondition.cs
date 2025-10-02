using UnityEngine;

[System.Serializable]
public class ConditionEntry
{
    public UnlockingCondition condition;
    public int intValue;
}

public abstract class UnlockingCondition : ScriptableObject
{
    public abstract bool CheckWithOverride(LevelStats stats, int value);
    public abstract string GenerateDefaultDescription(int value);
}