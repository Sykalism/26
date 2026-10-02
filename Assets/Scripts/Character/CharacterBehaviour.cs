using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Main Data")]
    public CharacterBaseData characterData;
    public GameObject weaponObject;
    

    [Header("Checkers")]
    [SerializeField] Transform groundChecker;
    public Transform climbingPoint;


    public bool autoFlip;

    
 
    //Input
    public ActionInput actionInput;

    //Actions
    public Knockback knockback {get; private set;}
    public Movement movement {get; private set;}
    public Dash dash {get; private set;}
    public Jump jump {get; private set;}
    public Attack attack {get; private set;}
    public Climb climb {get; private set;}
    public Die die {get; private set;}

    //Components
    public Rigidbody2D rb {get; private set;}
    public Weapon weapon {get; set;}
    public HealthPoint healthPoint {get; set;}
    private List<CharacterAction> actions;
    private ActionContext actContext;
    private ActionSelector selector;

    
    

    //Checkers
    public float footstepStrenght {get; set;}
    public float heightToGround {get; private set;}
    public bool isLanding {get; private set;}
    public bool isGrounded {get; private set;}
    public bool isTouchingObstacle {get; private set;}
    private bool beLanding;
    private float timer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthPoint = GetComponent<HealthPoint>();
        weapon = weaponObject.GetComponent<Weapon>();

        actContext = new ActionContext();
        actionInput = new ActionInput();
        selector = new ActionSelector();

        knockback = new Knockback(actContext, rb, healthPoint, characterData.knockbackForce, 0.2f);
        movement = new Movement(actContext, rb, characterData, transform, this);
        dash = new Dash(actContext, rb, characterData, this);
        jump = new Jump(actContext, rb, characterData, this);
        attack = new Attack(actContext, rb, characterData, weapon);
        climb = new Climb(actContext, rb, this);
        die = new Die(actContext, rb, healthPoint);

    }
    void Start()
    {
        actions = new List<CharacterAction>
        {
            knockback,
            movement,
            dash,
            jump,
            attack,
            climb,
            die
        };
    }
    void Update()
    {
        InitiateActionInput();
        if (groundChecker != null)
        {
            GroundCheck(groundChecker.position);
            ObstacleCheck(groundChecker.position);
        }
        Landing
        (
            transform.position, 
            characterData.HeightForLanding, 
            characterData.LandingDelay
        );
        selector.Execute(actions);
    }
    void FixedUpdate()
    {
        selector.FixedExecute();
    }
    private void InitiateActionInput()
    {
        movement.SetInput(actionInput.move, actionInput.direction, actionInput.run);
        dash.SetInput(actionInput.dash, actionInput.direction);
        jump.SetInput(actionInput.jump);
        attack.SetInput(actionInput.attack);
    }

    //Checkers Func
    public void GroundCheck(Vector2 origin)
    {
        isGrounded = Physics2D.Raycast(origin, Vector2.down, 0.1f, LayerMask.GetMask("Environment"));
    }
    public void ObstacleCheck(Vector2 origin)
    {
        float dir = transform.localScale.x > 0 ? 1 : -1;
        Vector2 offset = new Vector2(0.2f * dir, 0.1f);
        isTouchingObstacle = Physics2D.Raycast
        (
            origin + offset, 
            Vector2.up, 
            1.6f, 
            LayerMask.GetMask("Environment")
        );

    }
    public void Landing(Vector2 position, float height, float delay)
    {
        var ray = Physics2D.Raycast
        (
            position,
            Vector2.down,
            Mathf.Infinity
        );
        if (ray)
        {
            heightToGround = ray.distance;
        }

        if (heightToGround > height) beLanding = true;
        if (beLanding && heightToGround < 0.1f)
        {
            isLanding = true;
            beLanding = false;
        }
        if (isLanding)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0f;
                isLanding = false;
            }
        }
    }
    public void RippleInteract()
    {
        if (groundChecker == null) return;
        RippleManager ripple;

        Collider2D hit = Physics2D.OverlapCircle
        (
            groundChecker.position, 
            0.4f, 
            LayerMask.GetMask("Water")
        );
        if (hit)
        {
            ripple = hit.GetComponent<RippleManager>();
            ripple.AddRipple(groundChecker.position);
            ripple.rippleStrenght = 0.02f;
        }
    }
    //Events

    public void TriggerAttackEvent()
    {
        weapon.TriggerOn();
    }
    public void StopAttackEvent()
    {
        attack.StopAttackEvent();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (climb != null) {
            Gizmos.DrawLine(transform.position, climb.climbingTarget);
            Gizmos.DrawLine(
                climbingPoint.position, 
                new Vector2(
                    climbingPoint.position.x + 
                    transform.localScale.normalized.x * 
                    0.5f,
                    climbingPoint.position.y 
                )
            );
        }
    }
}

public class ActionInput
{
    public float direction;
    public bool 
    move,
    run,
    jump,
    dash,
    attack;


    public void InitiateInput
    (
        float direction,
        bool move = false, 
        bool run = false, 
        bool jump = false, 
        bool dash = false,
        bool attack = false
    )
    {
        this.direction = direction;
        this.move = move;
        this.run = run;
        this.jump = jump;
        this.dash = dash;
        this.attack = attack;
    }
}
