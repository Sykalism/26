using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    public PlayerData player = new PlayerData();
}

[Serializable]
public class PlayerData
{
    public float[] position;
    public float currentHealth;
}
