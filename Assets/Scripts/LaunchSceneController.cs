using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchSceneController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image logoImage;

    [Header("Timing")]
    [SerializeField] private float fadeInTime;
    [SerializeField] private float logoHoldTime;
    [SerializeField] private float fadeOutTime;

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        yield return Fade(1f, 0f, this.fadeInTime);

        yield return new WaitForSeconds(this.logoHoldTime);

        yield return Fade(0f, 1f, this.fadeOutTime);

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float time = 0f;
        Color color = this.fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float a = Mathf.Lerp(from, to, time / duration);
            this.fadeImage.color = new Color(color.r, color.g, color.b, a);
            yield return null;
        }

        this.fadeImage.color = new Color(color.r, color.g, color.b, to);
    }
}