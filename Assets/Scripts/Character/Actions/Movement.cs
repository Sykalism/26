using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.XR.ARFoundation.VisualScripting;

public class Movement : CharacterAction
{
    private Rigidbody2D rb;
    
    private float speed;
    private float moveSpeed = 0;
    private float runSpeed = 0f;
    private bool moveInput;
    private bool automaticFlip => character.autoFlip;
    public float direction {get; private set;}
    public bool isRunning {get; private set;}
    private Transform target;
    private Vector3 startScale;
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
        startScale = target.localScale;
    }
    public override void SetInput(bool inputA = false, float valueA = 0f, bool inputB = false, float valueB = 0f)
    {
        moveInput = inputA;
        direction = valueA;
        isRunning = inputB;
    }
    public override bool Condition()
    { 
        if (actionContext.isLocked) return false;
        return moveInput && !character.isLanding;
    }
    public override void Execute()
    {
        speed = isRunning? runSpeed : moveSpeed;
        if (automaticFlip)
        {
            Flip();
        }
    }
    public override void FixedExecute()
    {
        if (character.isTouchingObstacle && !character.isGrounded)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else 
        {
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }
    }
    public void Flip()
    {
        float flipValue = startScale.x;
        float dirX = direction;

        if (dirX != 0)
        {
            flipValue = dirX < 0? startScale.x * -1 : startScale.x;
            Vector3 newScale = new Vector3
            (
                flipValue,
                startScale.y,
                startScale.z
            );

            target.localScale = newScale;
        } 
    }
}
