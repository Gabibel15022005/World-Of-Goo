using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StarUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_Text text;
    public bool isAlreadyUnlocked = false;

    void Start()
    {
        if (PlayerPrefs.HasKey($"{SceneManager.GetActiveScene().name}{gameObject}"))
        {
            PlayerPrefs.GetInt($"{SceneManager.GetActiveScene().name}{gameObject}");
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
            PlayerPrefs.SetInt($"{SceneManager.GetActiveScene().name}{gameObject}", 1);
            PlayerPrefs.Save();
        }
    }
}
