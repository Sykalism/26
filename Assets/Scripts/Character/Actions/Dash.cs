using System.Reflection.Emit;
using UnityEngine;

public class Dash : CharacterAction
{
    private Rigidbody2D rb;
    private CharacterBehaviour character;
    private bool dashInput;
    private float dashVelocity;
    private float inputDirection;
    public float dashDirection {get; private set;}
    private GameTimer dashTimer;
    private bool isDashing;
    private bool stopDashing;
    private GameTimer cooldown;
    private float cooldownTime = 0.5f;
    public override bool IsExecuting => isDashing;
    public Dash
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterBaseData data,
        CharacterBehaviour character
    ) : base(context, 4)
    {
        this.rb = rb;
        this.character = character;
        dashVelocity = data.DashForce;
        dashTimer = new GameTimer(data.DashDuration);
        cooldown = new GameTimer(cooldownTime);
    }
    public void SetInput(bool dashInput, float direction)
    {
        this.dashInput = dashInput;
        inputDirection = direction;
    }
    public override bool Condition()
    {
        if(actionContext.isLocked) return false;
        if (!isDashing) cooldown.Tick(Time.deltaTime);
        dashDirection = inputDirection;
        return 
            dashInput 
            && !isDashing 
            && dashDirection != 0
            && !cooldown.IsRunning 
            && !character.isLanding;
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
        }
        if (!stopDashing) rb.linearVelocity = new Vector2(dashDirection * dashVelocity, 0f);
        else
        {
            rb.linearVelocity = Vector2.zero;
            isDashing = false;
        }
    }
}
