using UnityEngine;
using System.Collections.Generic;

public class LevelSelector : MonoBehaviour
{
    public List<string> scenes = new List<string>();

    [SerializeField] private GameObject prefabButton;
    [SerializeField] private Transform content;


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