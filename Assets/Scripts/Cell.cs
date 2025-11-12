using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Image background;
    private TextMeshProUGUI display;
    private Button button;
    public int CorrectNumber { get; private set; }
    public bool Hidden { get; set; } = false;

    public void SetCorrectNumber(int number)
    {
        this.display = GetComponentInChildren<TextMeshProUGUI>();
        this.button = GetComponent<Button>();
        this.CorrectNumber = number;
        this.display.text = this.CorrectNumber.ToString();

        this.button.onClick.AddListener(() => GameManager.Instance.SelectActiveCell(this));
        this.DeselectActive();
    }

    public void SetVisibility(bool enabled)
    {
        this.display.gameObject.SetActive(enabled);
        this.Hidden = enabled;
    }

    public bool Guess(short number)
    {
        if (this.CorrectNumber == number)
        {
            this.display.gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }

    public void SetAsActive()
    {
        this.background.color = this.activeColor;
    }

    public void DeselectActive()
    {
        this.background.color = this.defaultColor;
    }
}
