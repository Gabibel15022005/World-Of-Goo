using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool isScalledToZero = false;
    [SerializeField] private Animator animator;

    void Start()
    {
        if (isScalledToZero) animator.Play("ScaleZeroButton");
        else SpawnButton();
    }

    public void SpawnButton()
    {
        animator.Play("SpawnButton");
    }
}
