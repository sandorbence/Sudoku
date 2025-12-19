using System;
using UnityEngine;
using UnityEngine.UI;

public class MistakesDisplay : MonoBehaviour
{
    private Text display;

    private void Start()
    {
        this.display = GetComponent<Text>();
        this.OnSaveManagerDataChanged(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        SaveManager.DataChanged += this.OnSaveManagerDataChanged;
    }

    private void OnSaveManagerDataChanged(object sender, EventArgs e)
    {
        this.display.text = SaveManager.Data.GameState.Mistakes.ToString();
    }

    public void OnDestroy()
    {
        SaveManager.DataChanged -= this.OnSaveManagerDataChanged;
    }
}