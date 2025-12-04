using System;
using TMPro;

public class BestTimeDisplay : Singleton<BestTimeDisplay>
{
    private const string DisplayText = "Best time: ";
    private TextMeshProUGUI display;

    public void Set(float? time)
    {
        this.display = GetComponent<TextMeshProUGUI>();
        string formattedTime = time == null ? "None" :
            TimeSpan.FromSeconds((double)time).ToString(@"hh\:mm\:ss");
        this.display.text = $"{DisplayText}{formattedTime}";
    }
}