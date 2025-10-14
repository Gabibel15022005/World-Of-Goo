using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneWithString : MonoBehaviour
{
    [HideInInspector] public string scene;

    public void GoToThisScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
