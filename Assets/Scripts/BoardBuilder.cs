using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    [SerializeField] private Transform cellParent;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private float cellOffset;
    public static BoardBuilder Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void BuildBoard(Cell[,] filledBoard, int cellsToRemove)
    {
        int[,] board = new int[9, 9];
        System.Random random = new System.Random();
        float startX = -4 * this.cellOffset;
        float startY = -4 * this.cellOffset;

        this.FillBoardRandom(board, random);

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                GameObject cellObj = Instantiate(this.cellPrefab, this.cellParent);
                cellObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    startX + i * this.cellOffset,
                    startY + j * this.cellOffset);
                Cell cell = cellObj.GetComponent<Cell>();
                cell.SetCorrectNumber(board[i, j]);

                filledBoard[i, j] = cell;
            }
        }

        this.RemoveCells(board, cellsToRemove, new System.Random());

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (board[i, j] == 0) filledBoard[i, j].DeleteNumber();
            }
        }
    }

    private bool FillBoardRandom(int[,] board, System.Random random)
    {
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == 0)
                {
                    List<int> nums = Enumerable.Range(1, 9).OrderBy(x => random.Next()).ToList();
                    foreach (int num in nums)
                    {
                        if (IsValid(board, row, col, num))
                        {
                            board[row, col] = num;

                            if (FillBoardRandom(board, random)) return true;

                            board[row, col] = 0;
                        }
                    }

                    return false;
                }
            }
        }

        return true;
    }

    private bool IsValid(int[,] board, int row, int col, int num)
    {
        // Row check
        for (int c = 0; c < 9; c++)
        {
            if (board[row, c] == num) return false;
        }

        // Column check
        for (int r = 0; r < 9; r++)
        {
            if (board[r, col] == num) return false;
        }

        // 3x3 box check
        int boxRow = row - row % 3;
        int boxCol = col - col % 3;

        for (int r = 0; r < 3; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                if (board[boxRow + r, boxCol + c] == num) return false;
            }
        }

        return true;
    }

    private void RemoveCells(int[,] board, int removeCount, System.Random random)
    {
        List<int> positions = Enumerable.Range(0, 81).OrderBy(x => random.Next()).ToList();
        int removed = 0;

        for (int i = 0; i < positions.Count && removed < removeCount; i++)
        {
            int idx = positions[i];
            int row = idx / 9;
            int col = idx % 9;

            int backup = board[row, col];
            board[row, col] = 0;

            int[,] clone = (int[,])board.Clone();
            int solutionCount = CountSolutions(clone);

            if (solutionCount != 1)
            {
                board[row, col] = backup;
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
}