using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{
    public int scene;

    public void GoToThisScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
