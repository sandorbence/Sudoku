using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI text;

    private void Start()
    {
        this.button = GetComponent<Button>();
        this.text = GetComponentInChildren<TextMeshProUGUI>();

        this.button.onClick.AddListener(()
            => GameManager.Instance.WriteNumber(Convert.ToInt16(this.text.text)));
    }
}