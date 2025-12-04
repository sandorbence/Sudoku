using System;
using TMPro;
using UnityEngine;

public class GameTime : MonoBehaviour
{
    private TextMeshProUGUI display;

    public void SetVisibility(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }

    public void SetTime(float completionTime)
    {
        this.display = GetComponent<TextMeshProUGUI>();
        TimeSpan time = TimeSpan.FromSeconds(completionTime);
        this.display.text = $"Time: {time.ToString(@"hh\:mm\:ss")}";
    }
}