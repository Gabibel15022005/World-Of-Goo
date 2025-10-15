using UnityEngine;

public class Collectable : MonoBehaviour
{
    // call AddCollectableAction when collected

    private bool hasFoundGoo = false;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask gooLayerMask;


    void Update()
    {
        if (hasFoundGoo) return;

        hasFoundGoo = CheckIfHasFoundGoo();

        if (hasFoundGoo)
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        GameManager.AddCollectableAction?.Invoke();
        // do the logic for when collected in the child class
    }

    private bool CheckIfHasFoundGoo()
    {
        Collider2D[] goosCollider = Physics2D.OverlapCircleAll(transform.position, radius, gooLayerMask);

        foreach (Collider2D gooCollider in goosCollider)
        {
            if (gooCollider.TryGetComponent(out Goo goo))
            {
                if (goo != null && goo.CheckIfIsLinked())
                {
                    return true;
                }
            }
        }

        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
