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

        int[,] board = new int[9, 9];

        System.Random random = new System.Random();
        List<int> remainingIndexes = Enumerable.Range(0, 81).ToList();

        this.FillBoardRandom(board, random);

        for (int i = 0; i < 81; i++)
        {
            int row = i / 9;
            int col = i % 9;
            int value = board[row, col];

            GameObject cellObj = Instantiate(this.cellPrefab, this.cellParent);
            cellObj.GetComponent<RectTransform>()
                .anchoredPosition = new Vector2(startX + row * this.cellOffset, col * this.cellOffset);
            cellObj.GetComponent<Cell>().SetCorrectNumber(value);
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
}