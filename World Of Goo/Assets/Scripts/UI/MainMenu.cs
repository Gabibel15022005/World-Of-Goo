using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int levelSelector;
    public int visualTest;
    public void GoToLevelSelector()
    {
        GoToScene(levelSelector);
    }
    public void GoToVisualTest()
    {
        GoToScene(visualTest);
    }

    private void GoToScene(int sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}