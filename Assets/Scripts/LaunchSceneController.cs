using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchSceneController : MonoBehaviour
{
    public void OnFadeOutFinished()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}