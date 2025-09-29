using Unity.Mathematics;
using UnityEngine;

public class Goo : InteractableWithMouse
{
    private FixedJoint2D joint;
    private bool isStuck = false; public bool IsStuck => isStuck;
    private bool isLinked = false;
    private bool canBeLinked = false;

    [SerializeField] private float checkRadius = 1f;
    [SerializeField] private LayerMask layerMaskToCheck;

    private Collider2D closestGoo;
    private Collider2D secondClosestGoo;

    public LineRenderer preVisualiser1 ,  preVisualiser2;
    private bool gooAreLinked = false;


    [Space(30)]
    [Header("SpringJoint2D variable")]
    [SerializeField] private bool springBoolCollision;
    [SerializeField] private float springFrequency;
    [SerializeField] private JointBreakAction2D springBreakAction;
    [SerializeField] private float springFloatBreak;



    [Space(30)]
    [Header("LineRenderer variable")]
    [SerializeField] private GameObject linkPrefab;

    void Start()
    {
        CheckIfIsLinked();
    }

    protected override void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        isUnderTheMouse = hit.collider != null && hit.collider == collid;

        if (Input.GetMouseButtonDown(0) && isUnderTheMouse && !isLinked) isDragging = true;

        if (Input.GetMouseButtonUp(0) && canBeLinked)
            LinkThisGoo();

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            rb.simulated = true;
            isDragging = false;
        }

        if (isDragging) OnDrag();

        if (isUnderTheMouse && !isDragging && !isLinked) OnOverMouse();

        CheckAroundWhenDragged();
    }

    protected override void OnDrag()
    {
        base.OnDrag();
        Sticky.OnUnstickRequested?.Invoke(this);
    }

    private void CheckAroundWhenDragged()
    {
        closestGoo = null;
        secondClosestGoo = null;

        if (!isDragging)
        {
            canBeLinked = false;
            gooAreLinked = false;
            
            preVisualiser1.positionCount = 0;
            preVisualiser2.positionCount = 0;
            return;
        }

        Collider2D[] check = Physics2D.OverlapCircleAll(transform.position, checkRadius, layerMaskToCheck);

        // Exclure soi-même ET ne garder que les Goo déjà liés
        check = System.Array.FindAll(check, c =>
        {
            if (c == GetComponent<Collider2D>()) return false;
            if (c.TryGetComponent(out Goo g))
            {
                return g.CheckIfIsLinked(); // garde seulement si isLinked == true
            }
            return false;
        });

        if (canBeLinked = check.Length > 1)
        {
            System.Array.Sort(check, (a, b) =>
            {
                float distA = Vector2.SqrMagnitude((Vector2)a.transform.position - (Vector2)transform.position);
                float distB = Vector2.SqrMagnitude((Vector2)b.transform.position - (Vector2)transform.position);
                return distA.CompareTo(distB);
            });

            closestGoo = check[0];
            secondClosestGoo = check[1];
        }

        // ta vérification de lien entre les deux goo reste identique
        if (closestGoo != null && secondClosestGoo != null)
        {
            Goo closestGooScript = closestGoo.GetComponent<Goo>();
            Goo secondClosestGooScript = secondClosestGoo.GetComponent<Goo>();

            gooAreLinked =
                closestGooScript.CheckIfIsLinkedTo(secondClosestGoo.GetComponent<Rigidbody2D>(), closestGooScript.rb) ||
                secondClosestGooScript.CheckIfIsLinkedTo(closestGoo.GetComponent<Rigidbody2D>(), secondClosestGooScript.rb);
        }

        ReplicateOnDrawGizmo();
    }

    public void ChangeJoint(FixedJoint2D other, Rigidbody2D rigidbody2D)
    {
        joint = other;
        joint.connectedBody = rigidbody2D;
    }
    public void ChangeIsStuck(bool value)
    {
        isStuck = value;
    }

    public bool CheckIfIsLinked()
    {
        isLinked = TryGetComponent(out SpringJoint2D component);
        rb.freezeRotation = isLinked;

        //Debug.Log($"{gameObject}'s isLinked = {isLinked}");

        return isLinked;
    }

    public bool CheckIfIsLinkedTo(Rigidbody2D gooRb, Rigidbody2D thisGooRb)
    {
        SpringJoint2D[] springs = GetComponents<SpringJoint2D>();
        foreach (SpringJoint2D spring in springs)
        {
            if (spring.connectedBody == gooRb && rb == thisGooRb)
            {
                //Debug.Log($"spring.connectedBody ({spring.connectedBody}) == gooRb ({gooRb}) && rb ({rb}) == thisGooRb ({thisGooRb})");
                return true;
            }
        }

        return false;
    }

    private void LinkThisGoo()
    {
        if (gooAreLinked)
        {
            LinkTheGooTo(closestGoo);
            LinkTheGooTo(secondClosestGoo);
        }
        else
        {
            closestGoo.GetComponent<Goo>().LinkTheGooTo(secondClosestGoo);
            Destroy(gameObject);
        }
    }

    public void LinkTheGooTo(Collider2D gooCollider)
    {
        // ajoute le spring a ce script 
        SpringJoint2D newJoint = gameObject.AddComponent<SpringJoint2D>();

        // met les bon parametres (a mettre en variable dans la prefab)
        newJoint.enableCollision = springBoolCollision;
        newJoint.frequency = springFrequency;
        newJoint.breakAction = springBreakAction;
        newJoint.breakForce = springFloatBreak;

        // donne le rb de gooCollider
        newJoint.connectedBody = gooCollider.GetComponent<Rigidbody2D>();

        CheckIfIsLinked();

        // ajoute aussi les line renderer en enfant
        GameObject linkObj = Instantiate(linkPrefab, transform.position, quaternion.identity, transform);

        Link link = linkObj.GetComponent<Link>();

        link.joint = newJoint;
        link.target = gooCollider.transform;

        //Debug.Log($"Build link between {gameObject} and {gooCollider.name}");
    }

    void ReplicateOnDrawGizmo()
    {
        if (!isDragging) return;

        if (gooAreLinked)
        {

            if (closestGoo != null && secondClosestGoo != null)
            {
                preVisualiser1.positionCount = 2;
                preVisualiser2.positionCount = 2;

                Vector3[] positions1 = new Vector3[2];
                positions1[0] = transform.position;
                positions1[1] = closestGoo.transform.position;
                preVisualiser1.SetPositions(positions1);

                Vector3[] positions2 = new Vector3[2];
                positions2[0] = transform.position;
                positions2[1] = secondClosestGoo.transform.position;
                preVisualiser2.SetPositions(positions2);
            }
            else
            {
                preVisualiser1.positionCount = 0;
                preVisualiser2.positionCount = 0;
            }

        }
        else
        {
            if (closestGoo != null && secondClosestGoo != null)
            {
                preVisualiser1.positionCount = 2;
                preVisualiser2.positionCount = 0;

                Vector3[] positions = new Vector3[2];
                positions[0] = closestGoo.transform.position;
                positions[1] = secondClosestGoo.transform.position;
                preVisualiser1.SetPositions(positions);
            }
            else
            {
                preVisualiser1.positionCount = 0;
                preVisualiser2.positionCount = 0;
            }
        }
    }
    void OnDrawGizmos()
    {
        if (!isDragging) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

        Gizmos.color = Color.yellow;
        if (gooAreLinked)
        {
            if (closestGoo != null) Gizmos.DrawLine(transform.position, closestGoo.transform.position);
            if (secondClosestGoo != null) Gizmos.DrawLine(transform.position, secondClosestGoo.transform.position);
        }
        else
        {
            if (closestGoo != null && secondClosestGoo != null) Gizmos.DrawLine(secondClosestGoo.transform.position, closestGoo.transform.position);
        }

    }

}
