using UnityEngine;
using UnityEngine.UI;

public class Pencil : Singleton<Pencil>
{
    private Color activeBackgroundColor;
    private Color defaultBackgroundColor;
    private Button button;
    private Image background;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.background = GetComponent<Image>();
        this.defaultBackgroundColor = this.background.color;
        this.activeBackgroundColor = this.background.color;
        this.activeBackgroundColor.a = Constants.ActiveAlpha;
        this.button.onClick.AddListener(() => GameManager.Instance.ToggleNoteMode());
    }

    public void ToggleNoteModeDisplay(bool enabled)
    {
        this.background.color = enabled ? this.activeBackgroundColor : this.defaultBackgroundColor;
    }
}