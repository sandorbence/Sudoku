using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] protected ClosableWindow windowToToggle;
    protected Button button;

    protected virtual void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => this.windowToToggle.Show());
    }

    public virtual void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}