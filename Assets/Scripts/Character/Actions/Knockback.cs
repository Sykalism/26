using UnityEngine;

public class Knockback : CharacterAction
{
    private CharacterBehaviour character;
    private Rigidbody2D rb => character.rb;
    private Health health => character.stats.health;
    private float strength => character.data.KnockbackForce;
    private GameTimer KnockbackTimer;
    private bool isKnocking;
    public override bool IsExecuting => isKnocking;

    public Knockback
    (
        ActionContext context,
        CharacterBehaviour character

    )
    : base(context, 8)
    {
        this.character = character;
        KnockbackTimer = new GameTimer(character.data.KnockbackDuration);
    }

    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return health.IsTakingDamage;
    }
    public override void OnEnter()
    {
        actionContext.Lock();
        KnockbackTimer.Start();
        character.directional.flipLocked = true;
        StartAction = true;    
        isKnocking = true;    
    }
    public override void Execute()
    {
        KnockbackTimer.Tick(Time.deltaTime);
        if (!KnockbackTimer.IsRunning)
        {
            OnExit();
        }
    }
    public override void OnExit()
    {
        actionContext.Unlock();
        isKnocking = false;
        character.directional.flipLocked = false;
    }
    public override void FixedExecute()
    {
        if (StartAction)
        {
            StartAction = false;

            rb.linearVelocity = Vector2.zero;

            Vector2 force = new Vector2(
                strength * health.affectedDamage.x * -1 / 2,
                strength
            );

            rb.AddForce(force, ForceMode2D.Impulse);
        }
        
    }
}
