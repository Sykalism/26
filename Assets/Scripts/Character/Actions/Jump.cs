using UnityEngine;

public class Jump : CharacterAction
{

    private Rigidbody2D rb;
    private CharacterBehaviour behaviour;
    private float jumpForce;
    private bool canJump;
    private bool jumpInput;
    public override bool IsExecuting => canJump;
    public Jump
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterData data,
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
        if (jumpInput && behaviour.isGrounded)
        {
            canJump = true;
        }
        return canJump;
    }
    public override void Execute()
    {

    }
    public override void FixedExecute()
    {
        if (canJump)
        {
            canJump = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }

}
