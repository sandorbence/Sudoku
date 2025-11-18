using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    [SerializeField] private Color activeBackgroundColor;
    private Color defaultBackgroundColor;
    private Button button;
    private TextMeshProUGUI text;
    private Image background;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.text = GetComponentInChildren<TextMeshProUGUI>();
        this.background = GetComponent<Image>();
        this.defaultBackgroundColor = this.background.color;
        this.button.onClick.AddListener(()
            => GameManager.Instance.WriteNumber(Convert.ToInt16(this.text.text)));
        GameManager.Instance.AddInput(this);
    }

    public void ToggleNoteDisplay(bool enabled)
    {
        this.background.color = enabled ? this.activeBackgroundColor : this.defaultBackgroundColor;
    }
}