using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    private Text display;
    private const short StartingPoints = 1000;
    private const short MistakeDecrement = 25;

    public short Calculate(float completionTime, short mistakes)
    {
        this.display = GetComponent<Text>();
        short score = (short)(StartingPoints - completionTime - mistakes * MistakeDecrement);
        this.display.text = score.ToString();

        return score;
    }
}