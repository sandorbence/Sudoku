using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Image background;
    private TextMeshProUGUI display;
    private short correctNumber;

    private void Start()
    {
        this.display = GetComponentInChildren<TextMeshProUGUI>();
        this.display.gameObject.SetActive(false);
    }

    public void SetCorrectNumber(short number)
    {
        this.correctNumber = number;
        this.display.text = this.correctNumber.ToString();
    }

    public bool Guess(short number)
    {
        if (this.correctNumber == number)
        {
            this.display.gameObject.SetActive(true);
            return true;
        }

        return false;
    }

    public void OnMouseDown()
    {
        GameManager.Instance.SelectActiveCell(this);
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
