using System;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColorController : MonoBehaviour
{
    [SerializeField] private ThemeColorName colorName;
    private Image image;
    private Text text;

    private void Awake()
    {
        this.image = GetComponent<Image>();
        this.text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ThemeManager.Instance.ThemeChanged += this.OnThemeChanged;
        this.ApplyTheme();
    }

    private void OnThemeChanged(object sender, EventArgs e)
    {
        this.ApplyTheme();
    }

    private void ApplyTheme()
    {
        if (ThemeManager.CurrentTheme == null) return;

        Color color = ThemeManager.CurrentTheme.GetColorByName(this.colorName);

        if (this.image != null)
        {
            this.image.color = color;
        }

        if (this.text != null)
        {
            this.text.color = color;
        }
    }

    public void OnDestroy()
    {
        ThemeManager.Instance.ThemeChanged -= this.OnThemeChanged;
    }
}