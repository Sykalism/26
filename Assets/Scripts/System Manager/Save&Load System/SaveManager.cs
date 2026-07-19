using UnityEngine;
using System.Linq;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance {get; private set;} 
    private string folder;
    private ISaveable[] saveables;
    private string path;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        folder = Path.Combine(Application.persistentDataPath, "saves");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        saveables = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None)
                    .OfType<ISaveable>()
                    .ToArray();



    }

    public void Save(int slot)
    {
        path = GetSavePath(slot);

        SaveData data = new SaveData();

        foreach(ISaveable saveable in saveables)
        {
            saveable.Save(data);
        }

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(path, json);
    }
    public void Load(int slot)
    {
        path = GetSavePath(slot);

        if (!File.Exists(path)) return;

        string json = File.ReadAllText(path);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        foreach(ISaveable saveable in saveables)
        {
            saveable.Load(data);
        }
    }
    public void LoadLastSave()
    {
        
        int dataAmount = Directory.GetFiles(folder, "*.json").Length;

        if (dataAmount == 0)
        {
            Debug.LogError("save files were not found!");
            return;
        }
        DateTime newest = DateTime.MinValue;
        int newestSlot = -1;

        for (int i = 1; i <= dataAmount; i++)
        {
            string path = GetSavePath(i);
            if (!File.Exists(path)) continue;

            DateTime time = File.GetLastWriteTime(path);

            if (time > newest)
            {
                newest = time;

                newestSlot = i;
                Load(newestSlot);
            }
        }
        
    }
    private string GetSavePath(int slot)
    {
        return Path.Combine(folder, $"save_{slot}.json");
    }

}
public interface ISaveable
{
    public void Save(SaveData data);
    public void Load(SaveData data);
}