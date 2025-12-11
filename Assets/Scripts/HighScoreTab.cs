using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTab : MonoBehaviour
{
    [SerializeField] private Difficulty difficulty;
    [SerializeField] private Text title;
    [SerializeField] private Text time;
    [SerializeField] private Text score;

    private void Start()
    {
        this.title.text = this.difficulty.ToString();
        SaveManager.Data.HighScores.TryGetValue(this.difficulty, out HighScoreData data);

        if (data != null)
        {
            if (!data.Time.Equals(0f))
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(data.Time);
                this.time.text = timeSpan.Hours > 0
                    ? timeSpan.ToString(@"hh\:mm\:ss")
                    : timeSpan.ToString(@"mm\:ss");
            }

            if (!data.Score.Equals(0))
            {
                this.score.text = data.Score.ToString();
            }
        }
    }
}