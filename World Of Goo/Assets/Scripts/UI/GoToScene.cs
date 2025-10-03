using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GoToScene : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] private SceneAsset sceneAsset; // seulement visible dans l’éditeur
    #endif

    private string scene;

    private void OnValidate()
    {
    #if UNITY_EDITOR
        if (sceneAsset != null)
            scene = sceneAsset.name;
    #endif
    }
    public void GoToThisScene()
    {
        SceneManager.LoadSceneAsync(scene);
    }
}
