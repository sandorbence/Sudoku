using TMPro;
using UnityEngine;

public class MistakesDisplay : Singleton<MistakesDisplay>
{
    private TextMeshProUGUI display;
    public short Mistakes { get; private set; } = 0;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
    }

    public void IncrementMistakes()
    {
        this.Mistakes++;
        this.display.text = $"Mistakes: {this.Mistakes.ToString()}";
    }
}