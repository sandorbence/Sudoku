using TMPro;
using UnityEngine;

public class PauseMenuTitle : MonoBehaviour
{
    private TextMeshProUGUI title;

    private void Start()
    {
        this.title = GetComponent<TextMeshProUGUI>();
    }

    public void Set(string title)
    {
        this.title.text = title;
    }
}