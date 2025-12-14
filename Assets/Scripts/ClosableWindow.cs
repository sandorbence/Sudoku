using UnityEngine;
using UnityEngine.UI;

public class ClosableWindow : MonoBehaviour
{
    [SerializeField] protected Button cancelButton;

    protected virtual void Start()
    {
        this.Hide();
        this.cancelButton.onClick.AddListener(() => this.Hide());
    }

    public virtual void Show()
    {
        GameManager.Instance.SetActivePopup(this);
        this.gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnDestroy()
    {
        this.cancelButton.onClick.RemoveAllListeners();
    }
}