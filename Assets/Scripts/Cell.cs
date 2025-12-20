using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [Header("Normal display")]
    [SerializeField] private Color defaultBackgroundColor;
    [SerializeField] private TextAnchor defaultAlignment;
    [SerializeField] private int defaultFontSize;
    [SerializeField] private Image background;
    [Header("Incorrect display")]
    [SerializeField] private Color incorrectTextColor;
    [Header("Note display")]
    [SerializeField] private Color noteTextColor;
    [SerializeField] private Color noteBackgroundColor;
    [SerializeField] private TextAnchor noteAlignment;
    [SerializeField] private int noteFontSize;
    [Header("Sounds")]
    [SerializeField] private AudioSource cellSelectSound;
    [SerializeField] private AudioSource correctGuessSound;

    private Color activeBackgroundColor = ThemeManager.CurrentTheme.ThemeColors
        .First(x => x.Name.Equals(ThemeColorName.Tertiary)).Color;
    private Text display;
    private Button button;
    private Color defaultTextColor;
    private HashSet<short> notes = new HashSet<short>();
    private bool isActive = false;

    public short CorrectNumber { get; private set; }
    public bool Editable { get; private set; } = false;

    public void SetCorrectNumber(short number)
    {
        this.display = GetComponentInChildren<Text>();
        this.defaultTextColor = this.display.color;
        this.ChangeDisplayMode(defaultDisplay: true);
        this.display.text = number.ToString();
        this.button = GetComponent<Button>();
        this.CorrectNumber = number;

        this.button.onClick.AddListener(() =>
        {
            GameManager.Instance.SelectActiveCell(this);
            this.cellSelectSound.Play();
        });

        this.DeselectActive();
    }

    public void SetAsEditable()
    {
        this.Editable = true;
    }

    public bool Guess(short number)
    {
        if (!this.Editable) return true;

        this.ClearCell();
        bool isCorrect = this.CorrectNumber == number;
        this.ChangeDisplayMode(defaultDisplay: true);
        this.display.color = isCorrect ? this.defaultTextColor : this.incorrectTextColor;
        this.display.text = number.ToString();
        this.Editable = !isCorrect;

        return isCorrect;
    }

    public void MakeNote(short number)
    {
        if (!this.Editable) return;

        this.ChangeDisplayMode(defaultDisplay: false);
        this.notes.Add(number);
        this.DisplayNotes();
    }

    public void ClearCell()
    {
        if (!this.Editable) return;

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
        this.isActive = true;
    }

    public void DeselectActive()
    {
        this.background.color = this.notes.Count != 0 ? this.noteBackgroundColor : this.defaultBackgroundColor;
        this.isActive = false;
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
        this.background.color = this.isActive ? this.activeBackgroundColor :
            defaultDisplay ? this.defaultBackgroundColor : this.noteBackgroundColor;
        this.display.color = defaultDisplay ? this.defaultTextColor : this.noteTextColor;
        this.display.alignment = defaultDisplay ? this.defaultAlignment : this.noteAlignment;
        this.display.fontSize = defaultDisplay ? this.defaultFontSize : this.noteFontSize;
    }

    public void SetCellState(CellState state)
    {
        this.Editable = true;

        if (state.Number != 0)
        {
            this.Guess(state.Number);
            return;
        }

        this.ClearCell();

        if (state.Notes != null)
        {
            notes = new HashSet<short>(state.Notes);
            this.ChangeDisplayMode(defaultDisplay: false);
            this.DisplayNotes();
        }
    }

    public CellState GetCurrentState()
    {
        return new CellState
        {
            Number = (notes.Count != 0 || this.display.text.Equals(string.Empty)) ?
            (short)0 : Convert.ToInt16(this.display.text),
            Notes = notes.Count == 0 ? null : new HashSet<short>(notes)
        };
    }
}