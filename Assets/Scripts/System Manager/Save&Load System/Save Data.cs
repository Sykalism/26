using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public string lastSave;
    public SaveData()
    {
        lastSave = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    public PlayerData player = new PlayerData();
    public GameData game = new GameData();
    public List<BossData> bosses = new();
}

[Serializable]
public class PlayerData
{
    public float[] position;
    public float currentHealth;
}

[Serializable]
public class GameData
{
    public bool isGameOver;
}
[Serializable]
public class BossData
{
    public string ID;
    public bool isActive;
    public float[] position;
    public float currentHealth;
}
