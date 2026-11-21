using UnityEngine;

public class Endurance : CharacterStats
{
    public Endurance(float maxValue) : base(maxValue)
    {
        
    }
    public override void Add(float value)
    {
        base.Add(value);
        Debug.Log("endurance recovered");
    }
    public override void Remove(float value)
    {
        base.Remove(value);
        Debug.Log("endurance consumed");
    }
}
