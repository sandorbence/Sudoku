using UnityEngine;
using UnityEngine.UI;

public class DifficultyChooser : ClosableWindow
{
    [SerializeField] private Button easyButton;
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;

    protected override void Start()
    {
        base.Start();
        this.gameObject.SetActive(false);

        this.easyButton.onClick.AddListener(() => GameManager.Instance.StartNewGame(Difficulty.Easy));
        this.normalButton.onClick.AddListener(() => GameManager.Instance.StartNewGame(Difficulty.Normal));
        this.hardButton.onClick.AddListener(() => GameManager.Instance.StartNewGame(Difficulty.Hard));
    }


    public override void OnDestroy()
    {
        base.OnDestroy();

        this.easyButton.onClick.RemoveAllListeners();
        this.normalButton.onClick.RemoveAllListeners();
        this.hardButton.onClick.RemoveAllListeners();
    }
}