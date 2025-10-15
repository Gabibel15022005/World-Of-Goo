using TMPro;
using UnityEngine;

public class SelectLevelButton : MonoBehaviour
{
    [SerializeField] ButtonScript button;
    [SerializeField] GoToSceneWithString goToScene;
    [SerializeField] TMP_Text text;

    [SerializeField] Animator[] starsAnimators;

    public void SetButton(string scene, Transform transform)
    {
        goToScene.scene = scene;
        text.text = scene;
        gameObject.transform.SetParent(transform);
        button.SpawnButton();

        for (int i = 1; i < 4; i++)
        {
            if (PlayerPrefs.HasKey($"{scene} : star {i}"))
            {
                // show an unlocked star
                starsAnimators[i - 1].Play("StarUnLock");
            }
        }
    }

}
