using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void RestartScene()
    {
        Debug.Log($"SceneManager.GetActiveScene().buildIndex = {SceneManager.GetActiveScene().buildIndex}");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
