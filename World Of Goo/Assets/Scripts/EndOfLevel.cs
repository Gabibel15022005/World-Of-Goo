using System;
using Unity.Mathematics;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{

    
    [Header("End of level Parameter")]
    private bool hasReachedTheEnd = false;
    public bool HasReachedTheEnd => hasReachedTheEnd;

    [HideInInspector] public bool hasFoundGoo = false;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask gooLayerMask;

    [SerializeField] private float endTimer;
    private float timer;
    public static Action<Transform> PosEndOfLevel;

    [Space(30)]
    [Header("ShockWave Parameter")]
    [SerializeField] GameObject shockWavePrefab;
    [SerializeField] float shockWaveDuration = 0.75f;
    [SerializeField] float shockWaveStrength = 0.5f;



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
                StartEndOfLevel();
                return;
            }
        }
        else
        {
            timer = 0;
        }

    }

    private void StartEndOfLevel()
    {
        GameObject shockWaveGameObject = Instantiate(shockWavePrefab, transform.position, quaternion.identity);
        ShockWaveManager shockWave = shockWaveGameObject.GetComponent<ShockWaveManager>();
        shockWave.StartShockWave(shockWaveDuration , shockWaveStrength);
        PosEndOfLevel?.Invoke(transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
