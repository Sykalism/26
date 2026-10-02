using UnityEngine;

public class Climb : CharacterAction
{
    private Rigidbody2D rb;
    private CharacterBehaviour CBehaviour;
    private float climbingSpeed = 7f;
    private bool isClimbing;
    private Vector2 climbingPoint;
    public Vector2 climbingTarget {get; private set;}
    private float charDirection;
    public override bool IsExecuting => isClimbing;

    public Climb(ActionContext context, Rigidbody2D rb, CharacterBehaviour CBehaviour)
     : base(context, 8)
    {
        this.rb = rb;
        this.CBehaviour = CBehaviour;
        
    }

    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        climbingPoint = CBehaviour.climbingPoint.position;
        charDirection = CBehaviour.transform.localScale.x > 0 ? 1 : -1;
        return 
            !ObjectHitCheck() 
            && Obstacle() != null
            && !isClimbing 
            && CBehaviour.isTouchingObstacle 
            && rb.linearVelocity.y > 0.1f;
    }
    public override void OnEnter()
    {
        actionContext.Lock();
        StartAction = true;
        isClimbing = true;

        Debug.Log("start climbing");
        GetClimbingTarget();
    }
    public override void Execute()
    {

    }
    public override void OnExit()
    {
        actionContext.Unlock();
    }
    public override void FixedExecute()
    {
        if (StartAction)
        {
            rb.linearVelocity = Vector2.zero;
            StartAction = false;
        }
        if (isClimbing)
        {
            if (!ObjectHitCheck())
            {
                Vector2 direction = (climbingTarget - rb.position).normalized;
                rb.linearVelocity = direction * climbingSpeed;
            }

            if (Vector2.Distance(rb.position, climbingTarget) < 0.05f || ObjectHitCheck())
            {
                rb.linearVelocity = Vector2.zero;
                isClimbing = false;
            }

        }
    }

    private bool ObjectHitCheck()
    {
        return Physics2D.Raycast
        (
            climbingPoint,
            new Vector2(charDirection, 0),
            0.5f,
            LayerMask.GetMask("Environment")
        );
    }
    private Collider2D Obstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position, 
            Vector2.right * 
            charDirection, 
            0.2f, 
            LayerMask.GetMask("Environment")
        );

        return hit.collider;
    }
    private void GetClimbingTarget()
    {
        float distanceX = 0.2f;
        Bounds bounds = Obstacle().bounds;

        float targetY = bounds.max.y + 0.1f;
        float targetX = rb.position.x + charDirection * distanceX;

        Vector2 target = new Vector2(targetX, targetY);
        climbingTarget = target;
    }

}
