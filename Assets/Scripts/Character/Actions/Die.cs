using UnityEngine;

public class Die : CharacterAction
{
    public bool isDead {get; private set;}
    private HealthPoint health;
    private Rigidbody2D rb;
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
        return health.CurrentHealth <= 0f;
    }
    public override void Execute()
    {
        actionContext.Lock();
        isDead = true;
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
