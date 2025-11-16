using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Normal display")]
    [SerializeField] private Color activeBackgroundColor;
    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private TextAlignmentOptions defaultAlignment;
    [SerializeField] private int defaultFontSize;
    [SerializeField] private Image background;
    [Header("Incorrect display")]
    [SerializeField] private Color incorrectTextColor;
    [Header("Note display")]
    [SerializeField] private Color noteTextColor;
    [SerializeField] private Color noteBackgroundColor;
    [SerializeField] private TextAlignmentOptions noteAlignment;
    [SerializeField] private int noteFontSize;

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
        this.ChangeDisplayMode(defaultDisplay: true);
        this.display.text = number.ToString();
        this.button = GetComponent<Button>();
        this.CorrectNumber = number;

        this.button.onClick.AddListener(() => GameManager.Instance.SelectActiveCell(this));
        this.DeselectActive();
    }

    public bool Guess(short number)
    {
        if (!this.editable) return true;

        bool isCorrect = this.CorrectNumber == number;
        this.ChangeDisplayMode(defaultDisplay: true);
        this.display.color = isCorrect ? this.defaultTextColor : this.incorrectTextColor;
        this.display.text = number.ToString();
        this.editable = !isCorrect;

        return isCorrect;
    }

    public void MakeNote(short number)
    {
        if (!this.editable) return;

        this.ChangeDisplayMode(defaultDisplay: false);
        this.notes.Add(number);
        this.DisplayNotes();
    }

    public void DeleteNumber()
    {
        if (!this.editable) return;

        this.ChangeDisplayMode(defaultDisplay: true);
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
        short count = 0;
        StringBuilder text = new StringBuilder();

        foreach (short note in this.notes)
        {
            text.Append(note);
            text.Append(' ');
            count++;

            if (count % 3 == 0) text.Append("\n");
        }

        this.display.text = text.ToString();
    }

    private void ChangeDisplayMode(bool defaultDisplay)
    {
        this.background.color = defaultDisplay ? this.defaultBackgroundColor : this.noteBackgroundColor;
        this.display.color = defaultDisplay ? this.defaultTextColor : this.noteTextColor;
        this.display.alignment = defaultDisplay ? this.defaultAlignment : this.noteAlignment;
        this.display.fontSize = defaultDisplay ? this.defaultFontSize : this.noteFontSize;
    }
}