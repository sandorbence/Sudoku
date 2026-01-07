using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public class SaveManager
{
    private static readonly string savePath = Path.Combine(Application.persistentDataPath, "save.json");
    private static SaveData data;
    public static EventHandler DataChanged;

    public static SaveData Data
    {
        get => data;
        private set
        {
            data = value;
        }
    }

    public static void Save()
    {
        // Means we are in game
        if (Timer.Instance != null)
        {
            Data.GameState.Time = Timer.Instance.Get();
            Data.GameState.Cells = GameManager.Instance.GetCellStatesFromBoard();
        }

        string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        string encrypted = EncryptionUtility.Encrypt(json);
        File.WriteAllText(savePath, encrypted);
        DataChanged?.Invoke(null, EventArgs.Empty);
    }

    public static void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Creating new save file...");
            Data = new SaveData();
            Save();
            return;
        }

        try
        {
            Debug.Log("Loading save file...");
            string encrypted = File.ReadAllText(savePath);
            string json = EncryptionUtility.Decrypt(encrypted);
            Data = JsonConvert.DeserializeObject<SaveData>(json);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception encountered when loading save file" + ex.Message);
            Data = new SaveData();
        }
    }

    public static void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }

        Data = new SaveData();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (Data != null)
            return;

        Load();
    }
}