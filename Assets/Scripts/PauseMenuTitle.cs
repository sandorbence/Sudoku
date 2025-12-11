using UnityEngine;
using UnityEngine.UI;

public class PauseMenuTitle : MonoBehaviour
{
    private Text title;

    public void Set(string title)
    {
        this.title = GetComponent<Text>();
        this.title.text = title;
    }
}