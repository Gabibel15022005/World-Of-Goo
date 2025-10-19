using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Level
{
    public int buildIndex;
    public string sceneName;
}

public class LevelSelector : MonoBehaviour
{
    public List<Level> scenes = new List<Level>();

    [SerializeField] private GameObject prefabButton;
    [SerializeField] private Transform content;


    void Start()
    {
        PlayerPrefs.SetInt($"{scenes[0].sceneName} is unlocked", 1);
        PlayerPrefs.Save();
        InstantiateLevels();
    }

    void InstantiateLevels()
    {
        foreach (Level scene in scenes)
        {
            if (PlayerPrefs.HasKey($"{scene.sceneName} is unlocked"))
            {
                GameObject button = Instantiate(prefabButton);
                button.GetComponent<SelectLevelButton>().SetButton(scene.buildIndex, scene.sceneName, content);
            }
        }
    }
}