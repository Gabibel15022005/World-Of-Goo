using UnityEngine;

public class SnakeRandomMovement : MonoBehaviour
{
    [Header("Mouvement")]
    public float moveSpeed = 3f;          // Vitesse de déplacement
    public float rotationSpeed = 5f;      // Rotation du sprite
    public float zigzagAmplitude = 0.5f;  // Largeur du zigzag
    public float zigzagFrequency = 3f;    // Fréquence du zigzag

    [Header("Zone de mouvement (rectangle)")]
    public Vector2 rectCenter = Vector2.zero;
    public float rectWidth = 16f;
    public float rectHeight = 8f;
    public float minDestinationDistance = 3f;

    [Header("Orientation / Sprite")]
    public float rotationOffset = 0f;
    public bool useSmoothRotation = true;
    public bool flipX = false;
    public bool flipY = false;

    private SpriteRenderer spriteRenderer;
    private Vector2 startPosition;       // Position au début du trajet
    private Vector2 controlPoint;        // Point de contrôle pour Bézier (pour virage)
    private Vector2 targetPosition;      // Nouvelle destination
    private float travelProgress = 0f;   // 0 -> départ, 1 -> destination
    private Vector2 lastPosition;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        PickNewDestination(initial: true);
        ApplySpriteFlip();
        lastPosition = transform.position;
    }

    void Update()
    {
        MoveAlongBezier();
    }

    void MoveAlongBezier()
    {
        float totalDistance = Vector2.Distance(startPosition, targetPosition);
        if (totalDistance < 0.001f) return;

        // Avance le long de la courbe
        travelProgress += (moveSpeed / totalDistance) * Time.deltaTime;
        travelProgress = Mathf.Clamp01(travelProgress);

        // --- Trajectoire Bézier quadratique ---
        Vector2 bezierPos = Mathf.Pow(1 - travelProgress, 2) * startPosition +
                            2 * (1 - travelProgress) * travelProgress * controlPoint +
                            Mathf.Pow(travelProgress, 2) * targetPosition;

        // Tangente pour zigzag
        Vector2 tangent = 2 * (1 - travelProgress) * (controlPoint - startPosition) + 2 * travelProgress * (targetPosition - controlPoint);
        Vector2 perpDir = new Vector2(-tangent.y, tangent.x).normalized;

        // Zigzag sinusoïdal
        float offset = Mathf.Sin(Time.time * zigzagFrequency * Mathf.PI * 2f) * zigzagAmplitude;
        Vector2 finalPos = bezierPos + perpDir * offset;

        // Déplacement fluide
        transform.position = Vector2.Lerp(transform.position, finalPos, Time.deltaTime * 10f);

        // Rotation vers la direction réelle
        Vector2 moveDir = ((Vector2)transform.position - lastPosition).normalized;
        if (moveDir.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg + rotationOffset;
            if (useSmoothRotation)
            {
                float currentZ = transform.eulerAngles.z;
                float newZ = Mathf.LerpAngle(currentZ, angle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, newZ);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }

        lastPosition = transform.position;

        // Quand la progression atteint 1 → nouvelle destination
        if (travelProgress >= 1f)
        {
            PickNewDestination();
        }
    }

    void PickNewDestination(bool initial = false)
    {
        Vector2 newTarget;
        int safety = 0;
        do
        {
            float x = Random.Range(rectCenter.x - rectWidth / 2f, rectCenter.x + rectWidth / 2f);
            float y = Random.Range(rectCenter.y - rectHeight / 2f, rectCenter.y + rectHeight / 2f);
            newTarget = new Vector2(x, y);
            safety++;
        } while (!initial && Vector2.Distance(transform.position, newTarget) < minDestinationDistance && safety < 50);

        startPosition = transform.position;
        targetPosition = newTarget;

        // --- Point de contrôle Bézier ---
        Vector2 mid = (startPosition + targetPosition) / 2f;
        Vector2 controlOffset = Random.insideUnitCircle * (Vector2.Distance(startPosition, targetPosition) / 2f);
        controlPoint = mid + controlOffset;

        travelProgress = 0f;
    }

    void ApplySpriteFlip()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = flipX;
            spriteRenderer.flipY = flipY;
        }
    }

    void OnValidate()
    {
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) ApplySpriteFlip();
    }

    void OnDrawGizmosSelected()
    {
        // Zone rectangulaire
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(rectCenter, new Vector3(rectWidth, rectHeight, 0));

        // Destination
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPosition, 0.15f);

        // Minimum distance
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, minDestinationDistance);

        // Visualisation Bézier
        if (Application.isPlaying)
        {
            Vector2 prev = startPosition;
            for (int i = 1; i <= 20; i++)
            {
                float t = i / 20f;
                Vector2 point = Mathf.Pow(1 - t, 2) * startPosition + 2 * (1 - t) * t * controlPoint + Mathf.Pow(t, 2) * targetPosition;
                Gizmos.DrawLine(prev, point);
                prev = point;
            }
        }
    }
}
