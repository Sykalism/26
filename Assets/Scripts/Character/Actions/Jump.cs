using System.Collections;
using UnityEngine;

public class Jump : CharacterAction
{

    private Rigidbody2D rb => character.rb;
    private CharacterBehaviour character;
    private float jumpForce => character.data.JumpForce;
    private bool jumpInput;
    private bool isJumping;
    private int jumpAttemp;

    public override bool IsExecuting => isJumping;

    public Jump
    (
        ActionContext context, 
        CharacterBehaviour character
    ) : base(context, 2)
    {
        this.character = character;
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
            character.StartCoroutine(NextFrame());
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
    }
    private IEnumerator NextFrame()
    {
        yield return null;
        isJumping = false;
    }
    private bool CanJump()
    {
        if (character.isGrounded) jumpAttemp = 1;
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
