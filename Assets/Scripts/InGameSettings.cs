using UnityEngine;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() =>
        GameManager.Instance.ShowInGameSettings());
    }
}