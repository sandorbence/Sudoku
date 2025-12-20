using System;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Theme")]
public class Theme : ScriptableObject
{
    public ThemeColor[] ThemeColors;

    public Color GetColorByName(ThemeColorName colorName) => this.ThemeColors
        .First(x => x.Name.Equals(colorName)).Color;
}

[Serializable]
public class ThemeColor
{
    public ThemeColorName Name;
    public Color Color;
}