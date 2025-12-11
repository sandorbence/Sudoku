using UnityEngine;
using UnityEngine.UI;

public class ContinueGame : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.interactable = SaveManager.Data.GameState != null;
        this.button.onClick.AddListener(() => GameManager.Instance.ContinueGame());
    }
}