using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterData characterData;
    [SerializeField] Weapon weapon;
    [SerializeField] GameObject particleDash;
    [SerializeField] Transform particleDashSpawner;
    [Header("Checker")]
    public Transform checkPoint;

    //Input
    private Vector2 inputDirection;
    private bool moveInput;
    private bool runInput;
    private bool jumpInput;
    private bool attackInput;
    private bool dashInput;

    private Rigidbody2D rb;
    private HealthPoint health;
    private Animator animator;
    private List<CharacterAction> actions;
    private ActionContext actContext;
    private ActionSelector selector;
    private CharacterBehaviour behaviour;

    //Variables

    //Actions
    private Knockback knockback;
    private Movement movement;
    private Dash dash;
    private Jump jump;
    private Attack attack;
    private Die die;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthPoint>();
        animator = GetComponent<Animator>();

        
        actContext = new ActionContext();
        selector = new ActionSelector();
        behaviour = new CharacterBehaviour();

        knockback = new Knockback(actContext, rb, health, 30f, 0.2f);
        movement = new Movement(actContext, rb, characterData, transform, behaviour);
        dash = new Dash(actContext, rb, characterData, behaviour);
        jump = new Jump(actContext, rb, characterData, behaviour);
        attack = new Attack(actContext, rb, characterData, weapon);
        die = new Die(actContext, rb, health);

    }
    void Start()
    {
        actions = new List<CharacterAction>() 
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
        inputDirection = new Vector2(Input.GetAxis("Horizontal"), 0f);
        moveInput = inputDirection.x != 0f? true : false;
        runInput = Input.GetKey(KeyCode.LeftShift);
        jumpInput = Input.GetKeyDown(KeyCode.Z);
        attackInput = Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0);
        dashInput = Input.GetKeyDown(KeyCode.C);
        
        if (checkPoint != null)
        {
            behaviour.GroundCheck(checkPoint.position);
            behaviour.ObstacleCheck(checkPoint.position, inputDirection, 0.5f);
        }

        movement.SetInput(moveInput, inputDirection.x, runInput);
        dash.SetInput(dashInput, inputDirection.x);
        jump.SetInput(jumpInput);
        attack.SetInput(attackInput);

        behaviour.Landing(transform.position, characterData.HeightForLanding, characterData.LandingDelay);

        if (Input.GetKeyDown(KeyCode.K))
        {
            health.TakeDamage(20f);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            health.TakeHeal(20);
        }

        if (attack.IsExecuting)
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
        else animator.updateMode = AnimatorUpdateMode.Fixed;

        selector.Execute(actions);
        Animation();

        RuntimeDebug();
    }
    void FixedUpdate()
    {
        selector.FixedExecute();
    }
    private void Animation()
    {
        if (die.isDead)
        {
            animator.SetTrigger("isDead");
        }
        else
        {
            animator.SetFloat("move", Mathf.Abs(inputDirection.x));
            animator.SetFloat("verticalForce", rb.linearVelocity.y);
            animator.SetInteger("attacks", attack.currentAttack);
            animator.SetBool("isLanding", behaviour.isLanding);
            animator.SetBool("isGrounded", behaviour.isGrounded);
            animator.SetBool("isRunning", movement.isRunning);
            animator.SetBool("isAttacking", attack.isAttacking);
            animator.SetBool("isComboWindow", attack.isComboWindow);
            if (jump.Condition())
            {
                animator.SetTrigger("jump");
            }
            if (dash.StartAction) animator.SetTrigger("dash");
        }
        
    }

    //Events
    public void TriggerAttackEvent()
    {
        weapon.TriggerOn();
    }
    public void StopAttackAnimation()
    {
        attack.StopAnimationEvent();
    }
    private void SpawnParticleDash()
    {
        Instantiate
        (
            particleDash, 
            particleDashSpawner.position, 
            particleDashSpawner.rotation
        );
    }
    private void RuntimeDebug()
    {
    }

}
