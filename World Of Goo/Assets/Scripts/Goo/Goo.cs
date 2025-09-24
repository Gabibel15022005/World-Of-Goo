using UnityEngine;

public class Goo : InteractableWithMouse
{
    public string stickyTag = "StickySurface";
    private FixedJoint2D joint;
    private bool isStuck = false;

    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private LayerMask layerMaskToCheck;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isStuck && collision.gameObject.CompareTag(stickyTag))
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = collision.rigidbody;
                isStuck = true;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        CheckAroundWhenDragged();
    }

    private void CheckAroundWhenDragged()
    {
        if (!isDragging) return;

        Collider2D[] check = Physics2D.OverlapCircleAll(transform.position, checkRadius, layerMaskToCheck);

    }

    void OnDrawGizmos()
    {
        if (!isDragging) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
