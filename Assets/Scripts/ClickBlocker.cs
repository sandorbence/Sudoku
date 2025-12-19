using UnityEngine.UI;

public class ClickBlocker : Singleton<ClickBlocker>
{
    private Button button;

    protected override void Awake()
    {
        base.Awake();
        this.button = GetComponent<Button>();
        this.button.onClick.AddListener(() => GameManager.Instance.ClosePopups());
        this.Deactivate();
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