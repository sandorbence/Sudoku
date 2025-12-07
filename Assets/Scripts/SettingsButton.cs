public class SettingsButton : ToggleButton
{
    protected override void Start()
    {
        base.Start();
        this.button.onClick.AddListener(() =>
        GameManager.Instance.ShowInGameSettings());
    }
}