using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset levelSelectorScene; // seulement visible dans l’éditeur
    [SerializeField] private SceneAsset visualTestScene; // seulement visible dans l’éditeur
#endif
    
    private string levelSelector;
    private string visualTest;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (levelSelectorScene != null)
            levelSelector = levelSelectorScene.name;

        if (visualTestScene != null)
            visualTest = visualTestScene.name;
#endif
    }

    public void GoToLevelSelector()
    {
        GoToScene(levelSelector);
    }
    public void GoToVisualTest()
    {
        GoToScene(visualTest);
    }

    private void GoToScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}