using UnityEngine;

public class Climb : CharacterAction
{
    private Rigidbody2D rb => character.rb;
    private CharacterBehaviour character;
    private float climbingSpeed = 7f;
    private bool isClimbing;
    private Vector2 climbingPoint => character.climbingPoint.position;
    private IKTargetHandler handClimb;
    public Vector2 climbingTarget {get; private set;}
    private float charDirection => (int)character.directional.level;
    public override bool IsExecuting => isClimbing;

    public Climb(ActionContext context, CharacterBehaviour character)
     : base(context, 9)
    {
        this.character = character;
        handClimb = character.GetComponent<IKTargetHandler>();
    }

    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return CanClimb();
    }
    public override void OnEnter()
    {
        actionContext.Lock();
        StartAction = true;
        isClimbing = true;
        GetClimbingTarget();

        if (handClimb == null) return;
        handClimb.target.position = climbingTarget;
        handClimb.SetAllTarget(handClimb.target);


    }
    public override void Execute()
    {
        Debug.Log(isClimbing);
    }
    public override void OnExit()   
    {
        actionContext.Unlock();
        climbingTarget = Vector2.zero;

        if (handClimb != null) handClimb.ResetAllTarget();
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
        var check = Physics2D.Raycast
        (
            climbingPoint,
            new Vector2(charDirection, 0),
            0.5f,
            LayerMask.GetMask("Environment")
        );
        return check;
    }
    private Collider2D Obstacle()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            character.ObstacleAheadCheckpoint.position, 
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

        float targetY = bounds.max.y + 0.02f;
        float targetX = rb.position.x + charDirection * distanceX;

        Vector2 target = new Vector2(targetX, targetY);
        climbingTarget = target;
    }
    private bool CanClimb()
    {
        return
            !ObjectHitCheck() 
            && Obstacle() != null
            && !isClimbing 
            && character.isTouchingObstacle 
            && rb.linearVelocity.y > 0.1f;
            
    }

}
