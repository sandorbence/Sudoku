using UnityEngine;
using UnityEngine.UI;

public class Pencil : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => GameManager.Instance.TriggerNoteMode());
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }
}