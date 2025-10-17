using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateTransition : MonoBehaviour
{
    [SerializeField] GameObject image;
    [SerializeField] Animator animator;
    public static Action<int> ChangeSceneWithInt;
    private int scene;

    void Start() { image.SetActive(true); }
    void OnEnable() { ChangeSceneWithInt += StartTransition; }
    void OnDisable() { ChangeSceneWithInt -= StartTransition; }

    public void StartTransition(int newScene)
    {
        scene = newScene;
        animator.Play("FadeIn");
    }

    public void GoToScene() { SceneManager.LoadSceneAsync(scene); }
}
