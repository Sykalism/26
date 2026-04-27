using System;
using UnityEngine;

public class Movement : CharacterAction
{


    private Rigidbody2D rb;
    private float direction;
    private float speed;
    private float targetSpeed;
    private float acceleration;
    private float moveSpeed = 0;
    private float runSpeed = 0f;
    public bool isMove {get; private set;}
    public bool isRunning {get; private set;}
    private Transform target;
    private Vector3 startScale;
    private CharacterBehaviour behaviour;
    

    public Movement
    (
        ActionContext context, 
        Rigidbody2D rb, 
        CharacterData data,
        Transform self,
        CharacterBehaviour behaviour
    ) : base(context, 1)
    {
        this.rb = rb;
        target = self;
        this.behaviour = behaviour;
        moveSpeed = data.MovementSpeed;
        runSpeed = data.RunSpeed;
        acceleration = data.Acceleration;
        startScale = target.localScale;
    }
    public override void SetInput(bool inputA = false, float valueA = 0f, bool inputB = false, float valueB = 0f)
    {
        isMove = inputA;
        direction = valueA;
        isRunning = inputB;
    }
    public override bool Condition()
    { 
        if (actionContext.isLocked) return false;
        return isMove && !behaviour.isLanding && !behaviour.isTouchingObstacle;
    }
    public override void Execute()
    {
        targetSpeed = isRunning? runSpeed : moveSpeed;
        
        Flip();
    }
    public override void FixedExecute()
    {
        if (behaviour.isTouchingObstacle && !behaviour.isGrounded)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else 
        {
            speed = Mathf.MoveTowards(speed, targetSpeed, acceleration * Time.fixedDeltaTime);
            rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
        }
    }

        private void Flip()
    {

        float flipValue = startScale.x;
        float dirX = direction;
        const float minus = -1;

        if (dirX != 0)
        {
            flipValue = dirX < 0? startScale.x * minus : startScale.x;
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
