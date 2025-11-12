using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Cell activeCell;
    private Cell[,] filledBoard;
    private Difficulty difficulty;

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
        this.difficulty = diff;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == "Game")
        {
            int cellsToRemove = this.difficulty switch
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
}