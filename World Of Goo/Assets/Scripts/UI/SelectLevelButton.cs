using TMPro;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] ButtonScript button;
    [SerializeField] GoToScene goToScene;
    [SerializeField] TMP_Text text;

    [SerializeField] Animator[] starsAnimators;

    public void SetButton(int scene, string sceneName, Transform transform)
    {
        goToScene.scene = scene;
        text.text = sceneName;
        gameObject.transform.SetParent(transform);
        button.SpawnButton();

        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.HasKey($"{sceneName} : star {i}"))
            {
                // show an unlocked star
                starsAnimators[i - 1].Play("StarUnLock");
            }
        }
    }

}
