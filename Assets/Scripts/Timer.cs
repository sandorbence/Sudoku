using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI display;
    private float elapsedTime = 0f;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(this.elapsedTime);
        this.display.text = time.ToString(@"hh\:mm\:ss");
    }
}