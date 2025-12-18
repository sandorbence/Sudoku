using UnityEngine;

public class PauseMenuDisplay : Settings
{
    [SerializeField] private PauseMenuTitle title;
    [SerializeField] private GameScore score;
    [SerializeField] private Transform scoreParent;
    [SerializeField] private GameTime time;
    [SerializeField] private Transform timeParent;
    [SerializeField] private VolumeSetter soundVolume;
    [SerializeField] private VolumeSetter musicVolume;

    private const string GameEnded = "Game Over!";
    private const string GamePaused = "Game Paused";

    protected override void Start()
    {
        base.Start();
        this.cancelButton.onClick.AddListener(this.Hide);
    }

    public override void Show()
    {
        base.Show();
        bool gameEnded = GameManager.Instance.GameEnded;

        if (gameEnded)
        {
            float completionTime = Timer.Instance.Get();
            this.time.SetTime(completionTime);
            short score = this.score.Calculate(completionTime, SaveManager.Data.GameState.Mistakes);
            GameManager.Instance.SetScoreAndTime(score, completionTime);
        }

        this.title.Set(gameEnded ? GameEnded : GamePaused);
        this.scoreParent.gameObject.SetActive(gameEnded);
        this.timeParent.gameObject.SetActive(gameEnded);
        this.soundVolume.SetVisibility(!gameEnded);
        this.musicVolume.SetVisibility(!gameEnded);
    }

    public override void Hide()
    {
        base.Hide();
        GameManager.Instance.ResumeGame();
    }
}