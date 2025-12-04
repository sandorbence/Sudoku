using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDisplay : Singleton<PauseMenuDisplay>
{
    [SerializeField] private PauseMenuTitle title;
    [SerializeField] private GameScore score;
    [SerializeField] private GameTime time;
    [SerializeField] private Button cancelButton;
    [SerializeField] private VolumeSetter soundVolume;
    [SerializeField] private VolumeSetter musicVolume;

    private void Start()
    {
        this.gameObject.SetActive(false);
        this.cancelButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
    }

    public void Show(bool gameEnded)
    {
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

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}