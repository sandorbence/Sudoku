public class PlayerAction
{
    public Cell AffectedCell { get; set; }
    public CellState PreviousState { get; set; }

    public void Undo()
    {
        this.AffectedCell.SetCellState(this.PreviousState);
    }
}