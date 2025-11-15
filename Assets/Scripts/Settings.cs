using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject settingsMenu;

    private void Start()
    {
        this.button.onClick.AddListener(() => this.settingsMenu.SetActive(true));
    }
}