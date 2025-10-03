using UnityEngine;
using TMPro;

public class ChangeTimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    void OnEnable()
    {
        GameManager.UpdateTimerUIAction += UpdateTimerUI;
    }

    void OnDisable()
    {
        GameManager.UpdateTimerUIAction -= UpdateTimerUI;
    }

    void UpdateTimerUI(int minutes, int seconds)
    {
        if (text != null)
            text.text = $"{minutes:0}:{seconds:00}";
    }
}
