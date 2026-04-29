using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Knockback : CharacterAction
{
    private Rigidbody2D rb;
    private Transform self;
    private HealthPoint health;
    private float strength;
    private GameTimer KnockbackTimer;
    private bool isKnocking;
    public override bool IsExecuting => isKnocking;

    public Knockback
    (
        ActionContext context,
        Rigidbody2D rb, 
        Transform self, 
        HealthPoint health, 
        float strength,
        float duration
        )
    : base(context, 0)
    {
        this.rb = rb;
        this.self = self;
        this.health = health;
        this.strength = strength;

        KnockbackTimer = new GameTimer(duration);
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
    }
    public override void FixedExecute()
    {
        if (StartAction)
        {
            StartAction = false;

            rb.linearVelocity = Vector2.zero;

            float direction = Mathf.Sign(self.localScale.x);
            Vector2 force = new Vector2(strength * direction / 2, strength);

            rb.AddForce(force, ForceMode2D.Impulse);
        }
        
    }
}
