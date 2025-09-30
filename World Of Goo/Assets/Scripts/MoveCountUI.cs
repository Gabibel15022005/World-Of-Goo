using TMPro;
using UnityEngine;

public class MoveCountUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    void OnEnable()
    {
        GameManager.UpdateMoveCountUI += UpdateMoveCountUI;
    }

    void OnDisable()
    {
        GameManager.UpdateMoveCountUI -= UpdateMoveCountUI;
    }

    void UpdateMoveCountUI(int value)
    {
        if (text != null)
            text.text = $"{value}";
    }
}
