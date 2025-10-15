using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LevelSelector : MonoBehaviour
{
    #if UNITY_EDITOR
    [SerializeField] private SceneAsset[] scenesAsset; // seulement visible dans l’éditeur
    #endif  
    private List<string> scenes = new List<string>();

    [SerializeField] private GameObject prefabButton;
    [SerializeField] private Transform content;



    private void OnValidate()
    {
#if UNITY_EDITOR
        if (scenesAsset != null)
        {
            scenes.Clear();
            for (int i = 0; i < scenesAsset.Length; i++)
            {
                scenes.Add(scenesAsset[i].name);
            }
        }
#endif
    }

    void Start()
    {
        PlayerPrefs.SetInt($"{scenes[0]} is unlocked", 1);
        PlayerPrefs.Save();
        InstantiateLevels();
    }

    void InstantiateLevels()
    {
        foreach (string scene in scenes)
        {
            if (PlayerPrefs.HasKey($"{scene} is unlocked"))
            {
                GameObject button = Instantiate(prefabButton);
                button.GetComponent<SelectLevelButton>().SetButton(scene, content);
            }
        }
    }
}