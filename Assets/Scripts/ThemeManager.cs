using System.Linq;
using UnityEngine;

public class ThemeManager : Singleton<ThemeManager>
{
    [SerializeField] Theme[] themes;
    public static Theme CurrentTheme;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        this.ApplyTheme(SaveManager.Data.Theme);
    }

    public void ApplyTheme(string name)
    {
        Theme themeToApply = this.themes.FirstOrDefault(x => x.name.Equals(name));

        if (themeToApply is null)
        {
            themeToApply = this.themes.First(x => x.name.Equals("Default"));
        }

        CurrentTheme = themeToApply;
    }
}