using System;
using TMPro;
using UnityEngine;

public class HighScoreTab : MonoBehaviour
{
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI time;
    [SerializeField] private TextMeshProUGUI score;

    private void Start()
    {
        this.title.text = this.difficulty.ToString();
        SaveManager.Data.HighScores.TryGetValue(this.difficulty, out HighScoreData data);

        if (data != null)
        {
            if (!data.Time.Equals(0f))
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(data.Time);
                this.time.text = timeSpan.ToString(@"hh\:mm\:ss");
            }

            if (!data.Score.Equals(0))
            {
                this.score.text = data.Score.ToString();
            }
        }
    }
}