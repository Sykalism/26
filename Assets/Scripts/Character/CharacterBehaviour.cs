using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    [Header("Main Data")]
    public CharacterBaseData characterData;
    public GameObject weaponObject;
    

    [Header("Checkers")]
    [SerializeField] Transform checkpoint;

    //Input
    public ActionInput actionInput;

    //Actions
    public Knockback knockback {get; private set;}
    public Movement movement {get; private set;}
    public Dash dash {get; private set;}
    public Jump jump {get; private set;}
    public Attack attack {get; private set;}
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
            die
        };
    }
    void Update()
    {
        InitiateActionInput();
        if (checkpoint != null)
        {
            GroundCheck(checkpoint.position);
            ObstacleCheck(checkpoint.position, actionInput.direction, 0.5f);
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
    public void ObstacleCheck(Vector2 origin, float direction, float distance)
    {
        Vector2 offset = new Vector2(0f, 0.1f);
        isTouchingObstacle = Physics2D.Raycast(origin + offset, Vector2.right * Mathf.Sign(direction), distance, LayerMask.GetMask("Environment"));
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
        if (checkpoint == null) return;
        RippleManager ripple;

        Collider2D hit = Physics2D.OverlapCircle
        (
            checkpoint.position, 
            0.4f, 
            LayerMask.GetMask("Water")
        );
        if (hit)
        {
            ripple = hit.GetComponent<RippleManager>();
            ripple.AddRipple(checkpoint.position);
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
