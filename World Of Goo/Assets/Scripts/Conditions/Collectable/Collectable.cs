using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [Header("Collectable Behaviour Parameter")]
    private bool hasFoundGoo = false;
    private bool canBeCollected = true;
    [SerializeField] private float radius = 2;
    [SerializeField] private LayerMask gooLayerMask;

    [Space(30)]
    [Header("Dissolve effect Parameter")]
    [SerializeField] Material material;
    [SerializeField] private float dissolveTime = 0.75f;
    private static int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");


    void OnEnable()
    {
        EndOfLevel.PosEndOfLevel += DeactivateCollectable;
    }
    void OnDisable()
    {
        EndOfLevel.PosEndOfLevel -= DeactivateCollectable;
    }
    private void DeactivateCollectable(Transform transform)
    {
        canBeCollected = false;
    }

    void Start()
    {
        ResetMaterial();
    }

    void ResetMaterial()
    {
        material.SetFloat(_dissolveAmount, 0);
    }

    void Update()
    {
        if (hasFoundGoo || !canBeCollected) return;

        hasFoundGoo = CheckIfHasFoundGoo();

        if (hasFoundGoo)
        {
            Collect();
        }
    }

    protected virtual void Collect()
    {
        GameManager.AddCollectableAction?.Invoke();
        StartCoroutine(DissolveObject(0f,1.1f));
    }

    IEnumerator DissolveObject(float startPos, float endPos)
    {
        float lerpedAmount;
        float elapsedTime = 0;

        while (elapsedTime < dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            lerpedAmount = Mathf.Lerp(startPos, endPos, elapsedTime / dissolveTime);

            material.SetFloat(_dissolveAmount, lerpedAmount);
            yield return null;
        }
        material.SetFloat(_dissolveAmount, 0);
        gameObject.SetActive(false);
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
