using System.Reflection.Emit;
using UnityEngine;

public class Dash : CharacterAction
{
    private Rigidbody2D rb;
    private CharacterBehaviour behaviour;
    private bool dashInput;
    private float dashVelocity;
    private float inputDirection;
    private float direction;
    private GameTimer dashTimer;
    private bool isDashing;
    private bool stopDashing;
    private GameTimer cooldown;
    private float cooldownTime = 1.5f;
    public override bool IsExecuting => isDashing;
    public Dash
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterBehaviour behaviour,
        float duration,
        float velocity
    ) : base(context, 2)
    {
        this.rb = rb;
        this.behaviour = behaviour;
        dashVelocity = velocity;
        dashTimer = new GameTimer(duration);
        cooldown = new GameTimer(cooldownTime);
    }
    public override void SetInput(bool inputA = false, float valueA = 0, bool inputB = false, float valueB = 0)
    {
        dashInput = inputA;
        inputDirection = valueA;
    }
    public override bool Condition()
    {
        if(actionContext.isLocked) return false;
        if (!isDashing) cooldown.Tick(Time.deltaTime);
        if (inputDirection != 0f)
        {
            direction = Mathf.Sign(inputDirection);
        }
        return 
            dashInput 
            && direction != 0 
            && !isDashing 
            && !cooldown.IsRunning 
            && !behaviour.isLanding;
    }
    public override void OnEnter()
    {
        stopDashing = false;
        isDashing = true;
        StartAction = true;
        dashTimer.Start();
        actionContext.Lock();
    }
    public override void Execute()
    {
        dashTimer.Tick(Time.deltaTime);

        if (!dashTimer.IsRunning)
        {
            stopDashing = true;
        }
    }
    public override void OnExit()
    {
        cooldown.Start();
        actionContext.Unlock();
    }
    public override void FixedExecute()
    {
        if (StartAction) 
        {
            StartAction = false;
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        if (!stopDashing) rb.linearVelocity = new Vector2(direction * dashVelocity, 0f);
        else
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Dynamic;
            isDashing = false;
        }
    }
}
