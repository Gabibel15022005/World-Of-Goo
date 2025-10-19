using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isScaledToZero = false;
    [SerializeField] private Animator animator;

    void Start()
    {
        if (isScaledToZero) animator.Play("ScaleZeroButton");
        else SpawnButton();
    }

    public void SpawnButton()
    {
        animator.Play("SpawnButton");
    }
}
