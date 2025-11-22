using UnityEngine;
using UnityEngine.UI;

public class DifficultyChooser : Singleton<DifficultyChooser>
{
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    private void Start()
    {
        this.gameObject.SetActive(false);

        this.easyButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Easy));
        this.mediumButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Medium));
        this.hardButton.onClick.AddListener(() => GameManager.Instance.StartGame(Difficulty.Hard));
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        this.easyButton.onClick.RemoveAllListeners();
        this.mediumButton.onClick.RemoveAllListeners();
        this.hardButton.onClick.RemoveAllListeners();
    }
}