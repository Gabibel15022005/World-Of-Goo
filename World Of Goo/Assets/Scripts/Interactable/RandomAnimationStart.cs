using UnityEngine;

public class RandomAnimationStart : MonoBehaviour
{
    [SerializeField] private string idleStateName = "Idle"; // nom exact de ton state dans l'Animator
    [SerializeField, Range(0f, 1f)] private float maxOffset = 1f;

    [SerializeField] private Animator animator;

    void Start()
    {

        // Met un décalage aléatoire dans le cycle de l'animation Idle
        float randomOffset = Random.Range(0f, maxOffset);
        animator.Play(idleStateName, 0, randomOffset);
    }
}
