using TMPro;
using UnityEngine;
public class StarUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text text;

    public void SetDescription(string description)
    {
        text.text = description;
    }

    public void UnlockStar()
    {
        animator.Play("StarUnLock");
        Debug.Log($"Unlock this : {gameObject}");
    }
}
