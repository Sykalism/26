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
    public override void SetInput(bool inputA = false, float valueA = 0, bool inputB = false, float valueB = 0)
    {
        jumpInput = inputA;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return CanJump();
    }
    public override void Execute()
    {
    }
    public override void FixedExecute()
    {
        if (isJumping)
        {
            isJumping = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
    private bool CanJump()
    {
        if (behaviour.isGrounded) jumpAttemp = 1;
        if (jumpInput && jumpAttemp <= 2)
        {
            jumpAttemp += 1;
            isJumping = true;
            return true;
        }
        else
        {
            return false;
        }
    }

}
