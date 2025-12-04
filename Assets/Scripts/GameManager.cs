using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Cell activeCell;
    private Cell[,] filledBoard;
    private bool isInNoteMode = false;
    private static List<NumberInput> numberInputs = new List<NumberInput>();
    private Stack<PlayerAction> playerActions = new Stack<PlayerAction>();
    private HighScoreData highScoreData;

    public Difficulty Difficulty { get; private set; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += this.OnSceneLoaded;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
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

    public void StartNewGame(Difficulty diff)
    {
        this.Difficulty = diff;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
        SaveManager.Data.GameState = null;

        if (PauseMenuDisplay.Instance != null)
        {
            PauseMenuDisplay.Instance.Hide();
        }
    }

    public void ContinueGame()
    {
        this.Difficulty = SaveManager.Data.GameState.Difficulty;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            SaveManager.Data.HighScores.TryGetValue(this.Difficulty, out HighScoreData data);
            BestTimeDisplay.Instance.Set(data.Time);

            if (SaveManager.Data.GameState != null)
            {
                this.filledBoard = BoardBuilder.Instance.BuildBoardFromStartedGame(SaveManager.Data.GameState.Cells);
                Timer.Instance.Set(SaveManager.Data.GameState.Time);
                return;
            }

            int cellsToRemove = this.Difficulty switch
            {
                Difficulty.Easy => 40,
                Difficulty.Medium => 45,
                Difficulty.Hard => 50,
                _ => throw new Exception("Not a valid difficulty")
            };

            this.filledBoard = new Cell[9, 9];
            BoardBuilder.Instance.BuildBoard(this.filledBoard, cellsToRemove);
            SaveManager.Data.GameState = new GameState
            {
                Difficulty = this.Difficulty,
                Mistakes = 0,
                Cells = GetCellStatesFromBoard(this.filledBoard)
            };
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
            SaveManager.Data.GameState.Mistakes++;
            SaveManager.Save();
            return;
        }

        if (this.CheckForWin())
        {
            this.SetGameOver();
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

    public void BackToMain()
    {
        this.ClearInputs();
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    private void ClearInputs()
    {
        numberInputs.Clear();
    }

    private bool CheckForWin() => this.filledBoard.Cast<Cell>().All(x => !x.Editable);

    private void SetGameOver()
    {
        PauseMenuDisplay.Instance.Show(gameEnded: true);

        SaveManager.Data.GameState = null;

        if (!SaveManager.Data.HighScores.TryGetValue(this.Difficulty, out HighScoreData currentHighScore))
        {
            SaveManager.Data.HighScores[this.Difficulty] = this.highScoreData;
            SaveManager.Save();
            return;
        }

        if (this.highScoreData.Score > currentHighScore.Score)
        {
            Debug.Log("New high score!");
            SaveManager.Data.HighScores[this.Difficulty].Score = this.highScoreData.Score;
        }

        if ((int)this.highScoreData.Time < (int)currentHighScore.Time)
        {
            Debug.Log("New best time!");
            SaveManager.Data.HighScores[this.Difficulty].Time = this.highScoreData.Time;
        }

        SaveManager.Save();
    }

    public void ToggleNoteMode()
    {
        this.isInNoteMode = !this.isInNoteMode;
        Pencil.Instance.ToggleNoteModeDisplay(this.isInNoteMode);
        numberInputs.ForEach(i => i.ToggleNoteDisplay(this.isInNoteMode));
    }

    public void ShowInGameSettings()
    {
        Time.timeScale = 0f;
        PauseMenuDisplay.Instance.Show(gameEnded: false);
    }

    public void ResumeGame()
    {
        PauseMenuDisplay.Instance.Hide();
        Time.timeScale = 1f;
    }

    public void SetScoreAndTime(short score, float time)
    {
        this.highScoreData = new HighScoreData { Score = score, Time = time };
    }

    private static CellInfo[] GetCellStatesFromBoard(Cell[,] board) => board.Cast<Cell>()
        .Select(x => new CellInfo
        {
            State = x.GetCurrentState(),
            CorrectNumber = x.CorrectNumber
        })
        .ToArray();
}