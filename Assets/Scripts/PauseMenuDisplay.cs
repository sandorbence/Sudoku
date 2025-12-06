using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDisplay : Settings
{
    [SerializeField] private PauseMenuTitle title;
    [SerializeField] private GameScore score;
    [SerializeField] private GameTime time;
    [SerializeField] private VolumeSetter soundVolume;
    [SerializeField] private VolumeSetter musicVolume;

    private void Start()
    {
        this.Hide();
        this.cancelButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
    }

    public override void Show()
    {
        bool gameEnded = GameManager.Instance.GameEnded;

        if (gameEnded)
        {
            float completionTime = Timer.Instance.Get();
            this.time.SetTime(completionTime);
            short score = this.score.Calculate(completionTime, SaveManager.Data.GameState.Mistakes);
            GameManager.Instance.SetScoreAndTime(score, completionTime);
        }

        this.title.Set(gameEnded ? "Game Over!" : "Game Paused");
        this.score.SetVisibility(gameEnded);
        this.time.SetVisibility(gameEnded);
        this.soundVolume.SetVisibility(!gameEnded);
        this.musicVolume.SetVisibility(!gameEnded);
        this.gameObject.SetActive(true);
    }
}