using TMPro;
using UnityEngine;

public class VersionText : MonoBehaviour
{
    private TextMeshProUGUI display;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
        this.display.text = $"v{Application.version}";
    }
}