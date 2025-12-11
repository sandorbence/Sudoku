using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    private Text display;

    public void SetVisibility(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    public void SetTime(float completionTime)
    {
        this.display = GetComponent<Text>();
        TimeSpan time = TimeSpan.FromSeconds(completionTime);
        this.display.text = time.ToString(@"hh\:mm\:ss");
    }
}