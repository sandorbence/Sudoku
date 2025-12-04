using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int Version = 1;
    public float MusicVolume = 0.5f;
    public float SoundVolume = 0.5f;
    public Dictionary<Difficulty, HighScoreData> HighScores = new Dictionary<Difficulty, HighScoreData>();
    public GameState GameState;

    public float GetVolume(VolumeType type)
    {
        return type.Equals(VolumeType.Sound) ? this.SoundVolume : this.MusicVolume;
    }

    public void SetVolume(VolumeType type, float value)
    {
        if (type.Equals(VolumeType.Sound))
        {
            this.SoundVolume = value;
        }
        else
        {
            this.MusicVolume = value;
        }
    }
}