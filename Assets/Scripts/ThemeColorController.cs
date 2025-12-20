using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColorController : MonoBehaviour
{
    [SerializeField] private ThemeColorName colorName;
    private Image image;
    private Text text;

    private void Start()
    {
        try
        {
            this.image = GetComponent<Image>();
            this.image.color = ThemeManager.CurrentTheme.ThemeColors
                .First(x => x.Name.Equals(this.colorName)).Color;
        }
        catch (NullReferenceException)
        {
            this.text = GetComponent<Text>();
            this.text.color = ThemeManager.CurrentTheme.ThemeColors
                .First(x => x.Name.Equals(this.colorName)).Color;
        }
    }
}