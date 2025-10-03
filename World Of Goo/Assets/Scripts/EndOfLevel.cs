using System;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private bool hasReachedTheEnd = false;
    public bool HasReachedTheEnd => hasReachedTheEnd;

    [HideInInspector] public bool hasFoundGoo = false;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask gooLayerMask;

    [SerializeField] private float endTimer;
    private float timer;
    public static Action<Transform> PosEndOfLevel;
    void Update()
    {
        if (hasReachedTheEnd) return;

        hasFoundGoo = false;

        Collider2D[] goosCollider = Physics2D.OverlapCircleAll(transform.position, radius, gooLayerMask);

        foreach (Collider2D gooCollider in goosCollider)
        {
            if (gooCollider.TryGetComponent(out Goo goo))
            {
                if (goo != null && goo.CheckIfIsLinked())
                {
                    hasFoundGoo = true;
                    break;
                }
            }
        }

        if (hasFoundGoo)
        {
            timer += Time.deltaTime;

            if (timer >= endTimer)
            {
                hasReachedTheEnd = true;
                PosEndOfLevel?.Invoke(transform);
                return;
            }
        }
        else
        {
            timer = 0;
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
