using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColorController : MonoBehaviour
{
    [SerializeField] private ThemeColorName colorName;
    private Image image;

    private void Start()
    {
        this.image = GetComponent<Image>();
        this.image.color = ThemeManager.CurrentTheme.ThemeColors
            .First(x => x.Name.Equals(this.colorName)).Color;
    }
}