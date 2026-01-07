using UnityEngine;

public class AppLifecycleHandler : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveManager.Save();
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            SaveManager.Save();
        }
    }

    private void OnApplicationQuit()
    {
        SaveManager.Save();
    }
}
