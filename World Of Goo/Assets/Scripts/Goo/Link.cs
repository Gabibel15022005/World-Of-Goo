using NUnit.Framework.Constraints;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Link : MonoBehaviour
{
    private int length = 2;
    public LineRenderer lineRenderer;
    public Transform target;
    public SpringJoint2D joint;

    // public  DistanceJoint2D testJoint;
    // public bool UseSpring = false;

    private Vector3[] positions;
    [SerializeField] private BoxCollider2D linkCollider;

    void Start()
    {
        lineRenderer.positionCount = length;
        positions = new Vector3[length];
    }
    void Update()
    {
        positions[0] = transform.position;
        positions[1] = target.position;
        lineRenderer.SetPositions(positions);

        UpdateCollider();

        // if (UseSpring)
        // {
            if (!joint.enabled) Destroy(gameObject); 
        // }
        // else
        // {
        //     if (!testJoint.enabled) Destroy(gameObject); 
        // }
        
    }

    void UpdateCollider()
    {
        if (target == null || linkCollider == null) return;

        Vector2 start = transform.position;
        Vector2 end = target.position;
        Vector2 midPoint = (start + end) / 2f;

        linkCollider.transform.position = midPoint;

        float Dis = Vector2.Distance(start, end);
        linkCollider.size = new Vector2(Dis * 3.25f, 0.6f);

        Vector2 dir = end - start;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        linkCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (linkCollider.isTrigger) linkCollider.isTrigger = false;
    }

    void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}