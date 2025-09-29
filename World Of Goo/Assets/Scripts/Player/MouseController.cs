using UnityEngine;

public class MouseCameraController : MonoBehaviour
{
    public Vector2 centerLimits;
    public Vector2 sizeLimits;
    public Vector2 sizeDeadZone;

    public float moveSpeed = 5f;   // Vitesse de déplacement de la caméra

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        transform.position = NewCamPosition();
    }

    private Vector3 NewCamPosition()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 camPos = transform.position;

        Vector2 offset = mousePos - (Vector2)camPos;

        bool outsideX = Mathf.Abs(offset.x) > sizeDeadZone.x / 2f;
        bool outsideY = Mathf.Abs(offset.y) > sizeDeadZone.y / 2f;

        Vector3 targetPos = camPos;

        if (outsideX)
        {
            float directionX = Mathf.Sign(offset.x);
            targetPos.x += (Mathf.Abs(offset.x) - sizeDeadZone.x / 2f) * directionX;
        }

        if (outsideY)
        {
            float directionY = Mathf.Sign(offset.y);
            targetPos.y += (Mathf.Abs(offset.y) - sizeDeadZone.y / 2f) * directionY;
        }

        camPos = Vector3.Lerp(camPos, targetPos, moveSpeed * Time.deltaTime);

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        Vector2 halfLimits = sizeLimits / 2f;
        camPos.x = Mathf.Clamp(camPos.x, centerLimits.x - halfLimits.x + horzExtent, centerLimits.x + halfLimits.x - horzExtent);
        camPos.y = Mathf.Clamp(camPos.y, centerLimits.y - halfLimits.y + vertExtent, centerLimits.y + halfLimits.y - vertExtent);

        return new Vector3(camPos.x, camPos.y, transform.position.z);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, sizeDeadZone);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(centerLimits, sizeLimits);
    }
}