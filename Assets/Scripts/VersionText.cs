using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    private Text display;

    private void Start()
    {
        this.display = GetComponent<Text>();
        this.display.text = $"v{Application.version}";
    }
}