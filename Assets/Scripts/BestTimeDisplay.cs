using System;
using UnityEngine.UI;

public class BestTimeDisplay : Singleton<BestTimeDisplay>
{
    private Text display;

    public void Set(float? time)
    {
        this.display = GetComponent<Text>();

        if (time != null)
        {
            this.display.text = TimeSpan.FromSeconds((double)time).ToString(@"hh\:mm\:ss");
        }
    }
}