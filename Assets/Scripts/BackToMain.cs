using UnityEngine;
using UnityEngine.UI;

public class BackToMain : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => GameManager.Instance.BackToMain());
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}