using UnityEngine;

public abstract class CharacterStats
{
    public float max {get; protected set;}
    public float current {get; protected set;}

    public CharacterStats(float maxValue)
    {
        SetMax(maxValue);
        Add(max);
    }
    public void SetMax(float value)
    {
        max = value;
    }
    public virtual void Add(float value)
    {
        current += value;
        if (current > max)
        {
            current = max;
        }
    }
    public virtual void Remove(float value)
    {
        current -= value;
        if (current < 0)
        {
            current = 0; 
        }
    }
}
