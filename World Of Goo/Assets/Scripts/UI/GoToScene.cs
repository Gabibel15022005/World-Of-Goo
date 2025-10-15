using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GoToScene : MonoBehaviour
{
    public int scene;

    public void GoToThisScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
