using TMPro;
using UnityEngine;

public class MistakesDisplay : MonoBehaviour
{
    private TextMeshProUGUI display;
    public static MistakesDisplay Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayMistakes(short mistakes)
    {
        this.display.text = $"Mistakes: {mistakes.ToString()}";
    }
}