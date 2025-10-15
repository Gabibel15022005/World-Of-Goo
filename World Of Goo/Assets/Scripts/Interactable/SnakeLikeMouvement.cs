using UnityEngine;

public class SnakeLikeMouvement : InteractableWithMouse
{
    private Vector2 lastDirection = Vector2.left; // l’objet regarde à gauche par défaut

    protected override void OnDrag()
    {
        Vector2 currentPos = transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - currentPos);

        // Si la souris a vraiment bougé, on met à jour la direction
        if (direction.sqrMagnitude > 0.0001f)
        {
            lastDirection = direction.normalized;
        }

        // Calcule l'angle depuis la dernière direction connue
        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;

        // Comme ton sprite regarde à gauche → +180°
        float targetAngle = angle + 180f;

        // Applique la rotation (fluide)
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            Time.deltaTime * 20f
        );

        // Puis on déplace l’objet
        base.OnDrag();
    }

}
