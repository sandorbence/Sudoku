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
    private int correctNumber;

    public void SetCorrectNumber(int number)
    {
        this.display = GetComponentInChildren<TextMeshProUGUI>();
        this.correctNumber = number;
        this.display.text = this.correctNumber.ToString();
        //this.display.gameObject.SetActive(false);
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
