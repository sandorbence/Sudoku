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
        float startX = -4 * this.cellOffset;

        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 9; col++)
            {
                GameObject cellObj = Instantiate(this.cellPrefab, this.cellParent);
                cellObj.GetComponent<RectTransform>()
                    .anchoredPosition = new Vector2(startX + row * this.cellOffset, col * this.cellOffset);
            }
        }
    }

    public void SelectActiveCell(Cell cell)
    {
        this.activeCell.DeselectActive();
        this.activeCell = cell;
        this.activeCell.SetAsActive();
    }
}