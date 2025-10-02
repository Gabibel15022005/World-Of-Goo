using TMPro;
using UnityEngine;

public class MoveCountUI : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    void OnEnable()
    {
        GameManager.UpdateMoveCountUIAction += UpdateMoveCountUI;
    }

    void OnDisable()
    {
        GameManager.UpdateMoveCountUIAction -= UpdateMoveCountUI;
    }

    void UpdateMoveCountUI(int value)
    {
        if (text != null)
            text.text = $"{value}";
    }
}
