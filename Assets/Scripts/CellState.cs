using System.Collections.Generic;

public class CellState
{
    public short Guess { get; set; }
    public HashSet<short> Notes { get; set; }
}