using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartScene()
    {
        ActivateTransition.ChangeSceneWithInt(SceneManager.GetActiveScene().buildIndex);
    }
}
