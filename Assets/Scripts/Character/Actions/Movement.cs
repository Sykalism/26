using UnityEngine;

public class Movement : CharacterAction
{
    private Rigidbody2D rb;
    
    private float speed;
    private float moveSpeed = 0;
    private float runSpeed = 0f;
    private bool moveInput;
    public DirectionalLevel direction {get; private set;}
    public bool isRunning {get; private set;}
    private Transform target;
    private CharacterBehaviour character;
    

    public Movement
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterBaseData data,
        Transform self,
        CharacterBehaviour character
    ) : base(context, 1)
    {
        this.rb = rb;
        target = self;
        this.character = character;
        moveSpeed = data.MovementSpeed;
        runSpeed = data.RunSpeed;
    }
    public void SetInput(bool runInput, bool sprintInput, float inputDirection)
    {
        moveInput = runInput;
        direction = (DirectionalLevel)inputDirection;
        isRunning = sprintInput;
    }
    public override bool Condition()
    { 
        if (actionContext.isLocked) return false;
        return moveInput && !character.isLanding;
    }
    public override void Execute()
    {
        speed = isRunning? runSpeed : moveSpeed;

    }
    public override void FixedExecute()
    {
        if (character.isTouchingObstacle && !character.isGrounded)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else 
        {
            rb.linearVelocity = new Vector2((int)direction * speed, rb.linearVelocity.y);
        }
    }
}
