using UnityEngine;
public class Parry : CharacterAction
{
    private CharacterBehaviour character;
    private bool isParrying => parryInput;
    private bool parryInput;
    private Rigidbody2D rb => character.rb;

    public override bool IsExecuting => isParrying;

    public Parry(ActionContext context, CharacterBehaviour character) : base(context, 7)
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
    public override void OnEnter()
    {
        character.directional.flipLocked = true;
    }
    public override void Execute()
    {
        if (character.health.IsParried)
        {
            Debug.Log("parry succeed");
        }
    }
    public override void OnExit()
    {
        character.directional.flipLocked = false;
    }
    public override void FixedExecute()
    {
    }
}
