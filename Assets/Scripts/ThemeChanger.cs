using UnityEngine;

public class ThemeChanger : MonoBehaviour
{
    [SerializeField] private Transform displayParent;
    [SerializeField] private GameObject themeContentPrefab;
    [SerializeField] private float verticalOffset;

    private void Start()
    {
        Vector2 position = Vector2.zero;

        foreach (Theme theme in ThemeManager.Instance.Themes)
        {
            GameObject themeObject = Instantiate(this.themeContentPrefab, this.displayParent);
            RectTransform rect = themeObject.GetComponent<RectTransform>();
            rect.anchoredPosition = position;
            themeObject.GetComponent<ThemeContent>().SetData(theme);
            position += Vector2.down * verticalOffset;
        }
    }
}