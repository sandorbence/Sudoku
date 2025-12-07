using System;
using TMPro;

public class BestTimeDisplay : Singleton<BestTimeDisplay>
{
    private TextMeshProUGUI display;

    public void Set(float? time)
    {
        this.display = GetComponent<TextMeshProUGUI>();

        if (time != null)
        {
            this.display.text = TimeSpan.FromSeconds((double)time).ToString(@"hh\:mm\:ss");
        }
    }
}