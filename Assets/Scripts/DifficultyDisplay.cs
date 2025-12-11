using UnityEngine;
using UnityEngine.UI;

public class DifficultyDisplay : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        this.text = GetComponent<Text>();
        this.text.text = GameManager.Instance.Difficulty.ToString();
    }
}