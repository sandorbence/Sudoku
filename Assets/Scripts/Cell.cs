using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Normal display")]
    [SerializeField] private Color activeBackgroundColor;
    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private Image background;
    [Header("Incorrect display")]
    [SerializeField] private Color incorrectTextColor;
    [Header("Note display")]
    [SerializeField] private Color noteTextColor;
    [SerializeField] private Color noteBackgroundColor;

    private TextMeshProUGUI display;
    private Button button;
    private Color defaultTextColor;
    private bool editable = true;
    private HashSet<short> notes = new HashSet<short>();

    public int CorrectNumber { get; private set; }

    public void SetCorrectNumber(int number)
    {
        this.display = GetComponentInChildren<TextMeshProUGUI>();
        this.defaultTextColor = this.display.color;
        this.display.text = number.ToString();
        this.button = GetComponent<Button>();
        this.CorrectNumber = number;

        this.button.onClick.AddListener(() => GameManager.Instance.SelectActiveCell(this));
        this.DeselectActive();
    }

    public void SetVisibility(bool enabled)
    {
        this.display.gameObject.SetActive(enabled);
    }

    public bool Guess(short number)
    {
        if (!this.editable) return true;

        bool isCorrect = this.CorrectNumber == number;
        this.display.color = isCorrect ? this.defaultTextColor : this.incorrectTextColor;
        this.display.text = number.ToString();
        this.SetVisibility(isCorrect);
        this.editable = !isCorrect;

        return isCorrect;
    }

    public void MakeNote(short number)
    {
        if (!this.editable) return;

        this.notes.Add(number);
        this.background.color = this.noteBackgroundColor;
        this.display.color = this.noteTextColor;
        this.DisplayNotes();
        this.SetVisibility(true);
    }

    public void DeleteNumber()
    {
        if (!this.editable) return;

        this.background.color = this.defaultBackgroundColor;
        this.display.text = string.Empty;
        this.notes.Clear();
    }

    public void OnDestroy()
    {
        this.button.onClick.RemoveAllListeners();
    }

    public void SetAsActive()
    {
        this.background.color = this.activeBackgroundColor;
    }

    public void DeselectActive()
    {
        this.background.color = this.notes.Count != 0 ? this.noteBackgroundColor : this.defaultBackgroundColor;
    }

    private void DisplayNotes()
    {
        this.display.text = string.Join(" ", this.notes);

    }
}
