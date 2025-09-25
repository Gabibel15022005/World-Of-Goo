using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableWithMouse : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Collider2D collid;
    protected bool isDragging = false;
    protected bool isUnderTheMouse = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collid = GetComponent<Collider2D>();
    }

    protected virtual void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        isUnderTheMouse = hit.collider != null && hit.collider == collid;

        if (Input.GetMouseButtonDown(0) && isUnderTheMouse) isDragging = true;

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            rb.simulated = true;
            isDragging = false;
        }

        if (isDragging) OnDrag();

        if (isUnderTheMouse && !isDragging) OnOverMouse();

    }

    protected virtual void OnDrag()
    {
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }

    protected virtual void OnOverMouse()
    {
        //Afficher l'UI pour montrer qu'on peut intéragir avec
        //Debug.Log($"Is over {gameObject}");
    }
}
