using UnityEngine;

[CreateAssetMenu(fileName = "MoveCondition", menuName = "UnlockingConditions/MoveCondition")]
public class MoveCondition : UnlockingCondition
{
    public override bool CheckWithOverride(LevelStats stats, int value)
    {
        return stats.maxMoveNb <= value;
    }
    public override string GenerateDefaultDescription(int value)
    {
        return $"Finish the level in {value} move or less";
    }
}
