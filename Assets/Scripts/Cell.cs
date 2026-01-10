using System;
using System.Collections.Generic;
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
    [SerializeField] private TextAnchor noteAlignment;
    [SerializeField] private int noteFontSizeBig;
    [SerializeField] private int noteFontSizeMedium;
    [SerializeField] private int noteFontSizeSmall;
    [SerializeField] private int noteFontSizeVerySmall;
    [Header("Sounds")]
    [SerializeField] private AudioSource cellSelectSound;

    private Color activeBackgroundColor;
    private Color noteBackgroundColor;
    private Color noteActiveBackgroundColor;
    private Text display;
    private Button button;
    private Color defaultTextColor;
    private HashSet<short> notes = new HashSet<short>();
    private bool isActive = false;

    public short CorrectNumber { get; private set; }
    public bool Editable { get; private set; } = false;

    public void SetCorrectNumber(short number)
    {
        this.Setup();
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

    private void Setup()
    {
        this.display = GetComponentInChildren<Text>();
        this.defaultTextColor = this.display.color;
        this.activeBackgroundColor = ThemeManager.CurrentTheme.GetColorByName(ThemeColorName.Tertiary);
        this.activeBackgroundColor.a = Constants.ActiveAlpha;
        this.noteBackgroundColor = ThemeManager.CurrentTheme.GetColorByName(ThemeColorName.Secondary);
        this.noteBackgroundColor.a = Constants.NoteAlpha;
        this.noteActiveBackgroundColor = ThemeManager.CurrentTheme.GetColorByName(ThemeColorName.Secondary);
        this.noteBackgroundColor.a = Constants.ActiveAlpha;
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
        this.background.color = this.notes.Count == 0 ? this.activeBackgroundColor : this.noteActiveBackgroundColor;
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
        this.display.fontSize = count > 6 ? this.noteFontSizeVerySmall :
            count > 2 ? this.noteFontSizeSmall : count > 1 ?
            this.noteFontSizeMedium : this.noteFontSizeBig;
    }

    private void ChangeDisplayMode(bool defaultDisplay)
    {
        this.display.color = this.defaultTextColor;

        if (this.isActive)
        {
            this.background.color = defaultDisplay ?
                this.activeBackgroundColor : this.noteActiveBackgroundColor;
        }
        else
        {
            this.background.color = defaultDisplay ?
                this.defaultBackgroundColor : this.noteBackgroundColor;
        }

        this.display.alignment = defaultDisplay ? this.defaultAlignment : this.noteAlignment;
        this.display.fontSize = this.defaultFontSize;
    }

    public void SetCellState(CellState state)
    {
        this.Editable = true;

        if (state.Number != 0)
        {
            this.Guess(state.Number);
            return;
        }

        this.ChangeDisplayMode(defaultDisplay: true);
        this.ClearCell();

        if (state.Notes != null)
        {
            notes = new HashSet<short>(state.Notes);
            this.display.color = this.defaultTextColor;
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