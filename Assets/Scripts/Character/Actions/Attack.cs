using UnityEngine;

public class Attack : CharacterAction
{
    public int currentAttack {get; private set;}
    public bool isAttacking {get; private set;}
    public bool isComboWindow {get; private set;}
    private Rigidbody2D rb;
    private Transform hitPoint;
    private LayerMask targetMask;
    private float startGravity;
    private float hitRadius;
    private float damage;
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
        Transform hitPoint,
        LayerMask targetMask
    ) : base(context, 4)
    {
        this.rb = rb;
        this.hitPoint = hitPoint;
        this.targetMask = targetMask;
        hitRadius = data.HitRadius;
        damage = data.Damage;
        attackVariations = data.AttackVariant;

        startGravity = rb.gravityScale;

        comboTimer = new GameTimer(data.comboWindowTime);

    }
    public override void SetInput(bool inputA = false, float valueA = 0f, bool inputB = false, float valueB = 0)
    {
        attackInput = inputA;
    }
    public void StopAnimationEvent()
    {
        isAttacking = false;
        hasAttacked = false;
        isComboWindow = true;
        actionContext.Unlock();
        comboTimer.Start();
        rb.gravityScale = startGravity;
    }
    public void TriggerHitEvent()
    {
        Hit();
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
            currentAttack = 2;
        }
    }
    private void Hit()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll
        (
            hitPoint.position, 
            hitRadius, 
            targetMask
        );
        if (hit.Length > 0)
        {
            foreach(Collider2D target in hit)
            {
                HealthPoint enemyHP = target.GetComponent<HealthPoint>();
                if (enemyHP != null)
                {
                    enemyHP.TakeDamage(damage);
                }
            }
        }
    }
}
