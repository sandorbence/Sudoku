using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Version = 1;
    public float MusicVolume = 0.5f;
    public float SfxVolume = 0.5f;
    public Dictionary<Difficulty, HighScoreData> HighScores = new Dictionary<Difficulty, HighScoreData>();
}

[Serializable]
public class HighScoreData
{
    public short BestScore;
    public float BestTime;
}