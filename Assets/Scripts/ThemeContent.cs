using UnityEngine;
using UnityEngine.UI;

public class ThemeContent : MonoBehaviour
{
    [SerializeField] private Text themeName;
    [SerializeField] private Image primaryColor;
    [SerializeField] private Image secondaryColor;
    [SerializeField] private Image tertiaryColor;

    private Button button;

    public void SetData(Theme theme)
    {
        this.themeName.text = theme.name;
        this.primaryColor.color = theme.GetColorByName(ThemeColorName.Primary);
        this.secondaryColor.color = theme.GetColorByName(ThemeColorName.Secondary);
        this.tertiaryColor.color = theme.GetColorByName(ThemeColorName.Tertiary);

        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => ThemeManager.Instance.ApplyTheme(theme));
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}