using System;
using System.Collections.Generic;

[Serializable]
public class CellState
{
    public short Number { get; set; }
    public HashSet<short> Notes { get; set; }
}