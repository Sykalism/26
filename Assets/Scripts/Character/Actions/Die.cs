using UnityEngine;

public class Die : CharacterAction
{
    private bool isDead;
    private Health health => character.stats.health;
    private Rigidbody2D rb => character.rb;
    private CharacterBehaviour character;
    public override bool IsExecuting => isDead;
    public Die
    (
        ActionContext context, CharacterBehaviour character
    ) : base(context, 100)
    {
        this.character = character;
    }
    public override bool Condition()
    {
        isDead = health.current <= 0f ? true : false;
        return isDead;
    }
    public override void Execute()
    {
        if (health.current > 0f)
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
