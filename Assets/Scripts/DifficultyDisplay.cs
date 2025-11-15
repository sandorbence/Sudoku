using TMPro;
using UnityEngine;

public class DifficultyDisplay : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Start()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.text.text = GameManager.Instance.Difficulty.ToString();
    }
}