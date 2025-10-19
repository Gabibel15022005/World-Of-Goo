using System;
using UnityEngine;

public class Sticky : MonoBehaviour
{
    // Action statique qu'on peut appeler de n'importe où
    public static Action<Goo> OnUnstickRequested;

    private void OnEnable() { OnUnstickRequested += HandleUnstick; }

    private void OnDisable() { OnUnstickRequested -= HandleUnstick; }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Goo goo))
        {
            if (goo.IsStuck) return;
            //Debug.Log($"{collision.gameObject}");
            goo.ChangeJoint(gameObject.AddComponent<FixedJoint2D>(), collision.rigidbody);
            goo.ChangeIsStuck(true);
        }
    }
    private void HandleUnstick(Goo goo)
    {
        // Cherche tous les FixedJoint2D sur ce Sticky
        var joints = GetComponents<FixedJoint2D>();
        foreach (var joint in joints)
        {
            if (joint.connectedBody == goo.GetComponent<Rigidbody2D>())
            {
                //Debug.Log($"{goo.name} décollé via Action !");
                Destroy(joint);
                goo.ChangeIsStuck(false);
                break;
            }
        }
    }



}
