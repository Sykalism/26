using UnityEngine;

public class Jump : CharacterAction
{

    private Rigidbody2D rb;
    private CharacterBehaviour behaviour;
    private float jumpForce;
    private bool jumpInput;
    private bool isJumping;
    private int jumpAttemp;

    public override bool IsExecuting => isJumping;

    public Jump
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterBaseData data,
        CharacterBehaviour behaviour
    ) : base(context, 2)
    {
        this.rb = rb;
        jumpForce = data.JumpForce;
        this.behaviour = behaviour;
    }
    public void SetInput(bool jumpInput)
    {
        this.jumpInput = jumpInput;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return CanJump();
    }
    public override void OnEnter()
    {
        isJumping = true;
    }
    public override void Execute()
    {
    }
    public override void FixedExecute()
    {
        if (isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
        
    }
    private bool CanJump()
    {
        if (behaviour.isGrounded) jumpAttemp = 1;
        if (jumpInput && jumpAttemp <= 2 && !isJumping)
        {
            jumpAttemp += 1;
            return true;
        }
        else
        {
            return false;
        }
    }

}
