using System;
using System.Linq;
using UnityEngine;

public class ThemeManager : Singleton<ThemeManager>
{
    public Theme[] Themes;
    public static Theme CurrentTheme;
    public EventHandler ThemeChanged;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.Themes = Resources.LoadAll<Theme>("Themes");
        CurrentTheme = this.Themes.FirstOrDefault(x => x.name.Equals(SaveManager.Data.Theme));
        this.ApplyTheme(CurrentTheme);
    }

    public void ApplyTheme(Theme theme)
    {
        if (theme is null)
        {
            theme = this.Themes.First(x => x.name.Equals("Dark Forest"));
        }

        if (SaveManager.Data.Theme.Equals(theme.name)) return;

        CurrentTheme = theme;
        SaveManager.Data.Theme = theme.name;
        SaveManager.Save();
        ThemeChanged?.Invoke(this, new EventArgs());
    }
}