using System;

[Serializable]
public class GameState
{
    public Difficulty Difficulty;
    public CellInfo[] Cells;
    public short Mistakes;
    public float Time;
}