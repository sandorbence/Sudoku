public class Settings : ClosableWindow
{
    public static Settings Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        Instance = null;
    }
}