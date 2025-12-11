using System;
using UnityEngine;
using UnityEngine.UI;

public class MistakesDisplay : MonoBehaviour
{
    private Text display;

    private void Start()
    {
        this.display = GetComponent<Text>();
        SaveManager.DataChanged += this.OnSaveManagerDataChanged;
        this.OnSaveManagerDataChanged(this, EventArgs.Empty);
    }

    private void OnSaveManagerDataChanged(object sender, EventArgs e)
    {
        this.display.text = SaveManager.Data.GameState.Mistakes.ToString();
    }
}