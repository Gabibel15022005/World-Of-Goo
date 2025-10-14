using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class GoToScene : MonoBehaviour
{
    #if UNITY_EDITOR
    public SceneAsset sceneAsset; // seulement visible dans l’éditeur
    #endif

    [HideInInspector] public string scene;

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
