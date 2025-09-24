using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableWithMouse : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected bool isDragging = false;
    protected bool isUnderTheMouse = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        isUnderTheMouse = hit.collider != null && hit.collider == GetComponent<Collider2D>();

        if (Input.GetMouseButtonDown(0) && isUnderTheMouse) isDragging = true;

        if (Input.GetMouseButtonUp(0)) isDragging = false;

        if (isDragging) OnDrag();

        if (isUnderTheMouse && !isDragging) OnOverMouse();

    }

    private void OnDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        // move the object toward the mouse position
        Debug.Log($"Is holding on to {gameObject}");
    }

    protected virtual void OnOverMouse()
    {
        //Afficher l'UI pour montrer qu'on peut intéragir avec
        Debug.Log($"Is over {gameObject}");
    }
}
