using System;
using TMPro;
using UnityEngine;

public class MistakesDisplay : MonoBehaviour
{
    private TextMeshProUGUI display;

    private void Start()
    {
        this.display = GetComponent<TextMeshProUGUI>();
        SaveManager.DataChanged += this.OnSaveManagerDataChanged;
        this.OnSaveManagerDataChanged(this, EventArgs.Empty);
    }

    private void OnSaveManagerDataChanged(object sender, EventArgs e)
    {
        this.display.text = $"Mistakes: {SaveManager.Data.GameState.Mistakes.ToString()}";
    }
}