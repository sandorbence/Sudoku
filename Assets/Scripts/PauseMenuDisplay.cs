using UnityEngine;
using UnityEngine.UI;

public class PauseMenuDisplay : Singleton<PauseMenuDisplay>
{
    [SerializeField] private PauseMenuTitle title;
    [SerializeField] private GameScore score;
    [SerializeField] private Button cancelButton;

    private void Start()
    {
        this.gameObject.SetActive(false);
        this.cancelButton.onClick.AddListener(() => GameManager.Instance.ResumeGame());
    }

    public void Show(bool gameEnded)
    {
        this.title.Set(gameEnded ? "Game Over!" : "Game Paused");
        this.score.SetVisibility(gameEnded);
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}