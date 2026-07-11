using UnityEngine;

public class Attack : CharacterAction
{
    public int currentAttack {get; private set;}
    public bool isAttacking {get; private set;}
    public bool isComboWindow {get; private set;}
    private Rigidbody2D rb;
    private Weapon weapon;
    private float startGravity;
    private int attackVariations;
    private bool attackInput;
    private bool hasAttacked = false;
    private GameTimer comboTimer;
    public override bool IsExecuting => isAttacking;

    
    public Attack
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterData data,
        Weapon weapon
    ) : base(context, 5)
    {
        this.rb = rb;
        this.weapon = weapon;

        attackVariations = data.AttackVariant;
        this.weapon.damage = data.Damage;
        startGravity = rb.gravityScale;

        comboTimer = new GameTimer(data.comboWindowTime);

    }
    public override void SetInput(bool inputA = false, float valueA = 0f, bool inputB = false, float valueB = 0)
    {
        attackInput = inputA;
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
                comboTimer.Stop();
            }
        }
        return attackInput;
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
