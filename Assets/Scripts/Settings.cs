using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] protected Button cancelButton;
    public static Settings Instance { get; private set; }

    private void Start()
    {
        this.Hide();
        this.cancelButton.onClick.AddListener(() => this.Hide());
    }

    public virtual void Show()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void OnDestroy()
    {
        Instance = null;
    }
}