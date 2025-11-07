using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform cellParent;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float cellOffset;
    private Cell activeCell;

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
        this.BuildBoard();
    }

    public void SelectActiveCell(Cell cell)
    {
        this.activeCell.DeselectActive();
        this.activeCell = cell;
        this.activeCell.SetAsActive();
    }

    private void BuildBoard()
    {
        float startX = -4 * this.cellOffset;

        Cell[,] board = new Cell[9, 9];

        System.Random random = new System.Random();
        List<int> remainingIndexes = Enumerable.Range(0, 81).ToList();

        this.FillBoardRandom(board, random);

        //for (int i = 0; i < 81; i++)
        //{
        //    int row = i / 9;
        //    int col = i % 9;
        //    int value = board[row, col];

        //    GameObject cellObj = Instantiate(this.cellPrefab, this.cellParent);
        //    cellObj.GetComponent<RectTransform>()
        //        .anchoredPosition = new Vector2(startX + row * this.cellOffset, col * this.cellOffset);
        //    cellObj.GetComponent<Cell>().SetCorrectNumber(value);
        //}
    }

    private bool FillBoardRandom(Cell[,] board, System.Random random)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col].Hidden)
                {
                    List<int> nums = Enumerable.Range(1, 9).OrderBy(x => random.Next()).ToList();
                    foreach (int num in nums)
                    {
                        if (IsValid(board, row, col, num))
                        {
                            board[row, col].SetCorrectNumber(num);

                            if (FillBoardRandom(board, random)) return true;

                            board[row, col].SetCorrectNumber(0);
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    private bool IsValid(Cell[,] board, int row, int col, int num)
    {
        // Row check
        for (int c = 0; c < 9; c++)
        {
            if (board[row, c].CorrectNumber == num) return false;
        }

        // Column check
        for (int r = 0; r < 9; r++)
        {
            if (board[r, col].CorrectNumber == num) return false;
        }

        // 3x3 box check
        int boxRow = row - row % 3;
        int boxCol = col - col % 3;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[boxRow + r, boxCol + c].CorrectNumber == num) return false;
            }
        }

        return true;
    }

    private void RemoveCells(Cell[,] board, int removeCount, System.Random random)
    {
        List<int> positions = Enumerable.Range(0, 81).OrderBy(x => random.Next()).ToList();
        int removed = 0;

        for (int i = 0; i < positions.Count && removed < removeCount; i++)
        {
            int idx = positions[i];
            int row = idx / 9;
            int col = idx % 9;

            int backup = board[row, col].CorrectNumber;
            board[row, col].Hide();

            // Check if puzzle still has unique solution
            int[,] clone = (int[,])board.Clone();
            int solutionCount = CountSolutions(clone);

            if (solutionCount != 1)
            {
                // Revert removal if not unique
                board[row, col].SetCorrectNumber(backup);
            }
            else
            {
                removed++;
            }
        }
    }

    private int CountSolutions(int[,] board, int limit = 2)
    {
        int count = 0;
        this.SolveAndCount(board, ref count, limit);
        return count;
    }

    private void SolveAndCount(int[,] board, ref int count, int limit)
    {
        if (count >= limit) return;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == 0)
                {
                    for (int num = 1; num <= 9; num++)
                    {
                        if (this.IsValid(board, row, col, num))
                        {
                            board[row, col] = num;
                            this.SolveAndCount(board, ref count, limit);
                            board[row, col] = 0;
                        }
                    }
                    return;
                }
            }
        }

        count++;
    }

    public void ShowDifficultyChooser()
    {
        DifficultyChooser.Instance.Show();
    }

    public void StartGame(Difficulty diff)
    {
        int cellsToRemove = diff switch
        {
            Difficulty.Easy => 40,
            Difficulty.Medium => 45,
            Difficulty.Hard => 50,
            _ => throw new Exception("Not a valid difficulty")
        };

        Cell[,] board = this.BuildBoard();
        this.RemoveCells(board, cellsToRemove, new System.Random());
    }
}