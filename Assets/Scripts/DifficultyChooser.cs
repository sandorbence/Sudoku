using UnityEngine;
using UnityEngine.UI;

public class DifficultyChooser : MonoBehaviour
{
    [SerializeField] private Button easyButton;
    [SerializeField] private Button mediumButton;
    [SerializeField] private Button hardButton;

    public static DifficultyChooser Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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

    public void OnDestroy()
    {
        this.easyButton.onClick.RemoveAllListeners();
        this.mediumButton.onClick.RemoveAllListeners();
        this.hardButton.onClick.RemoveAllListeners();
    }
}