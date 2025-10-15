using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StarUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text text;
    public bool isAlreadyUnlocked = false;

    public int starNb = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name} : star {starNb}"))
        {
            isAlreadyUnlocked = true;
        }
    }

    public void SetDescription(string description)
    {
        text.text = description;

    }

    public void UnlockStar()
    {
        animator.Play("StarUnLock");
        Debug.Log($"Unlock this : {gameObject}");

        if (!isAlreadyUnlocked)
        {
            PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name} : star {starNb}", 1);
            PlayerPrefs.Save();
        }
    }
}
