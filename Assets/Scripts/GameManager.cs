using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Cell activeCell;
    private Cell[,] filledBoard;
    public Difficulty Difficulty { get; private set; }

    public static GameManager Instance;
    public short Mistakes { get; private set; } = 0;
    private bool isInNoteMode = false;

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
        if (this.isInNoteMode)
        {
            this.activeCell.MakeNote(number);
            return;
        }

        if (!this.activeCell.Guess(number)) this.Mistakes++;
    }

    public void Undo()
    {

    }

    public void Erase()
    {
        this.activeCell.DeleteNumber();
    }

    public void TriggerNoteMode()
    {
        this.isInNoteMode = !this.isInNoteMode;
    }
}