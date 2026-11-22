using System.Collections;
using UnityEngine;
public class Parry : CharacterAction
{
    private CharacterBehaviour character;
    private bool isParrying => Condition();
    private bool parryInput;
    private bool parried;
    private bool isRecovering;
    private Rigidbody2D rb => character.rb;

    public override bool IsExecuting => isParrying;
    public bool breakDefence => character.stats.endurance.current <= 0;

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
        return 
            parryInput && 
            !breakDefence &&
            character.isGrounded &&
            character.stats.endurance.current > 0f;
    }
    public override void OnEnter()
    {
        character.directional.flipLocked = true;
    }
    public override void Execute()
    {
        if (parried)
        {
            character.stats.endurance.Remove(character.data.ParryCost);
            character.StartCoroutine(BreakCooldown());
            parried = false;
        }
    }
    public override void OnExit()
    {
        character.directional.flipLocked = false;
    }
    public override void OnUpdate()
    {
        if (isRecovering) Recover(Time.deltaTime);
    }
    public void Parried()
    {
        parried = true;
    }
    private IEnumerator BreakCooldown()
    {
        isRecovering = false;
        yield return new WaitForSeconds(2);
        isRecovering = true;
    }
    public void Recover(float deltaTime)
    {
        if (character.stats.endurance.current >= character.stats.endurance.max)
        {
            isRecovering = false;
            return;
        }
        
        character.stats.endurance.Add(10 * deltaTime);
    }
}
