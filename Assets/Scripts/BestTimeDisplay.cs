using System;
using TMPro;

public class BestTimeDisplay : Singleton<BestTimeDisplay>
{
    private const string DisplayText = "Best time: ";
    private TextMeshProUGUI display;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
        this.display.text = $"{DisplayText}None";
    }

    public void Set(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        this.display.text = $"{DisplayText}{timeSpan.ToString(@"hh\:mm\:ss")}";
    }
}