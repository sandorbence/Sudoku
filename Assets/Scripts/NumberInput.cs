using System;
using UnityEngine;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    private Color activeBackgroundColor;
    private Color defaultBackgroundColor;
    private Button button;
    private Text text;
    private Image background;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.text = GetComponentInChildren<Text>();
        this.background = GetComponent<Image>();
        this.defaultBackgroundColor = this.background.color;
        this.activeBackgroundColor = this.background.color;
        this.activeBackgroundColor.a = Constants.ActiveAlpha;
        this.button.onClick.AddListener(()
            => GameManager.Instance.WriteNumber(Convert.ToInt16(this.text.text)));
        GameManager.Instance.AddInput(this);
    }

    public void ToggleNoteDisplay(bool enabled)
    {
        this.background.color = enabled ? this.activeBackgroundColor : this.defaultBackgroundColor;
    }
}