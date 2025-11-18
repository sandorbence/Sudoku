using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Cell activeCell;
    private Cell[,] filledBoard;
    private bool isInNoteMode = false;
    private static List<NumberInput> numberInputs = new List<NumberInput>();
    private Stack<PlayerAction> playerActions = new Stack<PlayerAction>();
    private short mistakes = 0;

    public Difficulty Difficulty { get; private set; }
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += this.OnSceneLoaded;
    }

    public void SelectActiveCell(Cell cell)
    {
        if (this.activeCell != null)
        {
            this.activeCell.DeselectActive();
        }

        this.activeCell = cell;
        this.activeCell.SetAsActive();
    }

    public void ShowDifficultyChooser()
    {
        DifficultyChooser.Instance.Show();
    }

    public void StartGame(Difficulty diff)
    {
        this.Difficulty = diff;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            int cellsToRemove = this.Difficulty switch
            {
                Difficulty.Easy => 40,
                Difficulty.Medium => 45,
                Difficulty.Hard => 50,
                _ => throw new Exception("Not a valid difficulty")
            };

            this.filledBoard = new Cell[9, 9];
            BoardBuilder.Instance.BuildBoard(this.filledBoard, cellsToRemove);
        }
    }

    public void WriteNumber(short number)
    {
        this.PushPlayerAction();

        if (this.isInNoteMode)
        {
            this.activeCell.MakeNote(number);
            return;
        }

        if (!this.activeCell.Guess(number))
        {
            MistakesDisplay.Instance.DisplayMistakes(++this.mistakes);
        }
    }

    public void Undo()
    {
        if (this.playerActions.Count == 0) return;

        PlayerAction lastPlayerAction = this.playerActions.Pop();
        lastPlayerAction.Undo();
    }

    public void Erase()
    {
        this.PushPlayerAction();
        this.activeCell.ClearCell();
    }

    public void AddInput(NumberInput input)
    {
        numberInputs.Add(input);
    }

    private void PushPlayerAction()
    {
        CellState currentCellState = this.activeCell.GetCurrentState();
        this.playerActions.Push(new PlayerAction { AffectedCell = this.activeCell, PreviousState = currentCellState });
    }

    private void ClearInputs()
    {
        numberInputs.Clear();
    }

    public void ToggleNoteMode()
    {
        this.isInNoteMode = !this.isInNoteMode;
        Pencil.Instance.ToggleNoteModeDisplay(this.isInNoteMode);
        numberInputs.ForEach(i => i.ToggleNoteDisplay(this.isInNoteMode));
    }
}