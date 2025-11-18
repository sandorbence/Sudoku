using UnityEngine;
using UnityEngine.UI;

public class Pencil : MonoBehaviour
{
    [SerializeField] private Color activeBackgroundColor;
    private Color defaultBackgroundColor;
    private Button button;
    private Image background;

    public static Pencil Instance { get; private set; }

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
        this.button = GetComponent<Button>();
        this.background = GetComponent<Image>();
        this.defaultBackgroundColor = this.background.color;
        this.button.onClick.AddListener(() => GameManager.Instance.ToggleNoteMode());
    }

    public void ToggleNoteModeDisplay(bool enabled)
    {
        this.background.color = enabled ? this.activeBackgroundColor : this.defaultBackgroundColor;
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}