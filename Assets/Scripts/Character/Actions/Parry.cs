using UnityEngine;
public class Parry : CharacterAction
{
    private CharacterBehaviour character;
    private bool isParrying => parryInput;
    private bool parryInput;
    private Rigidbody2D rb => character.rb;
    public override bool IsExecuting => isParrying;

    public Parry(ActionContext context, CharacterBehaviour character) : base(context, 8)
    {
        this.character = character;
    }
    public void SetInput(bool parryInput)
    {
        this.parryInput = parryInput;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return parryInput && character.isGrounded;
    }
    public override void Execute()
    {
        
    }
    public override void FixedExecute()
    {
    }
}
