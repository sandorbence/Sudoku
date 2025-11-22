using System;
using TMPro;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    private TextMeshProUGUI display;
    private const short StartingPoints = 1000;
    private const short MistakeDecrement = 25;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
    }

    public short Calculate(float completionTime, short mistakes)
    {
        short score = (short)(StartingPoints - completionTime - mistakes * MistakeDecrement);
        this.display.text = $"Score: {score.ToString()}";

        return score;
    }

    public void SetVisibility(bool enabled)
    {
        this.gameObject.SetActive(enabled);
    }
}