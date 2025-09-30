using System;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private bool hasReachedTheEnd = false;
    public bool HasReachedTheEnd => hasReachedTheEnd;

    private bool hasFoundGoo = false;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask gooLayerMask;

    [SerializeField] private float endTimer;
    private float timer;
    public static Action<Transform> PosEndOfLevel;


    // check if un goo linked est entré dans un OverlapCircleAll
    // si oui alors hasReachedTheEnd = true ( permet de déclencher l'animation de fin de niveau)


    // une fois fini les goo vont etre aspirer vers ce game object
    // envoie un Event pour donner une destination (ce gameobject) aux goo 
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
