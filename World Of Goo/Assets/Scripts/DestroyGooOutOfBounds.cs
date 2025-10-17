using System.Collections;
using UnityEngine;

public class DestroyGooOutOfBounds : MonoBehaviour
{
    public LayerMask gooLayer;
    Collider2D myCollider;
    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInLayerMask(collision.gameObject, gooLayer))
        {
            // Vérifie si la position du collider qui sort est encore dans ton trigger
            if (myCollider.OverlapPoint(collision.transform.position))
            {
                Debug.Log("Encore à l'intérieur !");
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask mask)
    {
        return ((1 << obj.layer) & mask) != 0;
    }
}
