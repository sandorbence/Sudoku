using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Color activeColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Image background;
    private TextMeshProUGUI display;
    public int CorrectNumber { get; private set; }
    public bool Hidden { get; set; } = false;

    public void SetCorrectNumber(int number)
    {
        this.display = GetComponentInChildren<TextMeshProUGUI>();
        this.CorrectNumber = number;
        this.display.text = this.CorrectNumber.ToString();
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
        this.Hidden = true;
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
