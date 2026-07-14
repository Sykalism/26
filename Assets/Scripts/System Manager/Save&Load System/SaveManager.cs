using UnityEngine;
using System.Linq;
using System.IO;

public class SaveManager : MonoBehaviour
{
    private ISaveable[] saveables;
    private string path;

    void Awake()
    {
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
    private string GetSavePath(int slot)
    {
        string folder = Path.Combine(Application.persistentDataPath, "saves");

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        return Path.Combine(folder, $"save_{slot}.json");
    }

}
public interface ISaveable
{
    public void Save(SaveData data);
    public void Load(SaveData data);
}