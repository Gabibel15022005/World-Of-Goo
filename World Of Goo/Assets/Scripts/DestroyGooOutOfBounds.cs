using UnityEngine;

public class DestroyGooOutOfBounds : MonoBehaviour
{
    public LayerMask gooLayer;
    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInLayerMask(collision.gameObject, gooLayer))
        {
            Destroy(collision.gameObject);
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }
}
