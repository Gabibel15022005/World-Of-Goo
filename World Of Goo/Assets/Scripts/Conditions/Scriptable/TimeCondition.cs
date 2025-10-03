using UnityEngine;

[CreateAssetMenu(fileName = "TimeCondition", menuName = "UnlockingConditions/TimeCondition")]
public class TimeCondition : UnlockingCondition
{
    public override bool CheckWithOverride(LevelStats stats, int value)
    {
        return stats.timeTaken <= value;
    }

    public override string GenerateDefaultDescription(int value)
    {
        return $"Finish the level in {value} seconds or less";
    }
}