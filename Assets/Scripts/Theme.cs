using System;
using UnityEngine;

[CreateAssetMenu(menuName = "UI/Theme")]
public class Theme : ScriptableObject
{
    public ThemeColor[] ThemeColors;
}

[Serializable]
public class ThemeColor
{
    public ThemeColorName Name;
    public Color Color;
}