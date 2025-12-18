using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    private Text display;

    public void SetTime(float completionTime)
    {
        this.display = GetComponent<Text>();
        TimeSpan time = TimeSpan.FromSeconds(completionTime);
        this.display.text = time.ToString(@"hh\:mm\:ss");
    }
}