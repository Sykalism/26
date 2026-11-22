using UnityEngine;

public class Attack : CharacterAction
{
    public int currentAttack {get; private set;}
    private bool isAttacking;
    public bool isComboWindow {get; private set;}
    private Rigidbody2D rb => character.rb;
    private Weapon weapon => character.weapon;
    private CharacterBehaviour character;
    private float startGravity;
    private int attackVariations => character.data.AttackVariant;
    private bool attackInput;
    private bool hasAttacked = false;
    public GameTimer comboTimer {get; private set;}
    public override bool IsExecuting => isAttacking;

    
    public Attack
    (
        ActionContext context, 
        CharacterBehaviour character
    ) : base(context, 5)
    {
        this.character = character;

        startGravity = rb.gravityScale;

        comboTimer = new GameTimer(character.data.comboWindowTime);

    }
    public void SetInput(bool attackInput)
    {
        this.attackInput = attackInput;


    }
    public void StopAttackEvent()
    {
        isAttacking = false;
        hasAttacked = false;
        isComboWindow = true;
        weapon.TriggerOff();
        actionContext.Unlock();
        comboTimer.Start();
        rb.gravityScale = startGravity;

        character.directional.flipLocked = false;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return !isAttacking && attackInput;
    }
    public override void OnEnter()
    {
        actionContext.Lock();
        isAttacking = true;
        StartAction = true;
        
        character.directional.flipLocked = true;
    }
    public override void Execute()
    {
        if (!hasAttacked)
        {
            ComboStep();
            hasAttacked = true;
        }
    }
    public override void FixedExecute()
    {
        if (StartAction)
        {
            rb.linearVelocity = Vector2.zero;
            StartAction = false;
        }
        if (isAttacking)
        {
            rb.gravityScale = 1f;
        }
    }
    public override void OnUpdate()
    {
        if (!isAttacking)
        {
            comboTimer.Tick(Time.deltaTime);
            if (!comboTimer.IsRunning)
            {
                isComboWindow = false;
                currentAttack = 0;
            }

        }
    }
    private void ComboStep()
    {
        currentAttack += 1;
        if (currentAttack > attackVariations)
        {
            currentAttack = 1;
        }
    }
}
