using UnityEngine;

public abstract class CharacterCondition
{
    public bool ActionInput {get; protected set; }
    public abstract void CheckCondition();
}
