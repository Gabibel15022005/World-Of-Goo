using UnityEngine;

[CreateAssetMenu(fileName = "CollectableCondition", menuName = "UnlockingConditions/CollectableCondition")]
public class CollectableCondition : UnlockingCondition
{
    public override bool CheckWithOverride(LevelStats stats, int value)
    {
        return stats.collectableFound >= value;
    }
    public override string GenerateDefaultDescription(int value)
    {
        return $"Find at least {value} collectable";
    }
}
