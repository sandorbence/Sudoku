using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : Singleton<Timer>
{
    private Text display;
    private float elapsedTime = 0f;

    private void Start()
    {
        this.display = GetComponent<Text>();
    }

    private void Update()
    {
        this.elapsedTime += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(this.elapsedTime);
        this.display.text = time.ToString(@"hh\:mm\:ss");
    }

    public void Set(float value)
    {
        this.elapsedTime = value;
    }

    public float Get()
    {
        return this.elapsedTime;
    }
}