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
        string json = JsonConvert.SerializeObject(Data, Formatting.Indented);
        //string encrypted = EncryptionUtility.Encrypt(json);
        File.WriteAllText(savePath, json);
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
            string json = File.ReadAllText(savePath);
            //string json = EncryptionUtility.Decrypt(encrypted);
            Data = JsonConvert.DeserializeObject<SaveData>(json);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception encountered when loading save file");
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

    public static void AutoSave() => Save();

    private static void SetupAutoSave()
    {
        Application.quitting += Save;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (Data != null)
            return;

        Load();
        SetupAutoSave();
    }
}