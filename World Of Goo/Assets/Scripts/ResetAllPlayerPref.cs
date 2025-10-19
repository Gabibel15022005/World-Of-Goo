using UnityEngine;

public class ResetAllPlayerPref : MonoBehaviour
{
    [SerializeField] GoToScene goToScene;
    public void ResetPlayerPref()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        goToScene.GoToThisScene();
    }
}
