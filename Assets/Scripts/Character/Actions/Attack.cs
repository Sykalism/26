using UnityEngine;

public class Attack : CharacterAction
{
    public int currentAttack {get; private set;}
    private bool isAttacking;
    public bool isComboWindow {get; private set;}
    private Rigidbody2D rb;
    private Weapon weapon;
    private CharacterBehaviour character;
    private float startGravity;
    private int attackVariations;
    private bool attackInput;
    private bool hasAttacked = false;
    public GameTimer comboTimer {get; private set;}
    public override bool IsExecuting => isAttacking;

    
    public Attack
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterBehaviour character,
        Weapon weapon
    ) : base(context, 5)
    {
        this.rb = rb;
        this.weapon = weapon;

        attackVariations = character.data.AttackVariant;
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
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        if (!isAttacking)
        {
            comboTimer.Tick(Time.deltaTime);
            if (!comboTimer.IsRunning)
            {
                isComboWindow = false;
                currentAttack = 0;
            }
            return attackInput;
        }
        else return false;
    }
    public override void OnEnter()
    {
        actionContext.Lock();
        isAttacking = true;
        StartAction = true;
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
    private void ComboStep()
    {
        currentAttack += 1;
        if (currentAttack > attackVariations)
        {
            currentAttack = 1;
        }
    }
}
