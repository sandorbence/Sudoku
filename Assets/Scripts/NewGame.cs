using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    private Button button;

    void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() =>
        {
            GameManager.Instance.ShowDifficultyChooser();
        });
    }
}
