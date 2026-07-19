using UnityEngine;

public class Die : CharacterAction
{
    public bool isDead {get; private set;}
    private HealthPoint health;
    private Rigidbody2D rb;
    public override bool IsExecuting => isDead;
    public Die
    (
        ActionContext context, Rigidbody2D rb, HealthPoint health
    ) : base(context, 100)
    {
        this.rb = rb;
        this.health = health;
    }
    public override bool Condition()
    {
        isDead = health.CurrentHealth <= 0f ? true : false;
        return isDead;
    }
    public override void Execute()
    {
        if (health.CurrentHealth > 0f)
        {
            isDead = false;
            actionContext.Unlock();
        }
        else actionContext.Lock();
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
