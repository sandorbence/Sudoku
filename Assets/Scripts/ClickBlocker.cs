using UnityEngine.UI;

public class ClickBlocker : Singleton<ClickBlocker>
{
    private Button button;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => GameManager.Instance.ClosePopup());
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        this.button.onClick.RemoveAllListeners();
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}