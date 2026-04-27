using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AIController : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    [SerializeField] CharacterBehaviour behaviour;
    [SerializeField] GameObject target;
    [SerializeField] Transform hitPoint;

    private Rigidbody2D rb;
    private Animator animator;
    private ActionSelector selector;
    private List<CharacterAction> actions;
    private List<CharacterCondition> conditions;
    private ActionContext context;
    private HealthPoint health;

    //Actions
    private Movement movement;
    private Attack attack;
    private Die die;
    //Conditions
    private CanMove canMove;
    private CanAttack canAttack;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthPoint>();
        selector = new ActionSelector();
        context = new ActionContext();
        behaviour = new CharacterBehaviour();

        movement = new Movement(context, rb, characterData, transform, behaviour);
        attack = new Attack(context, rb, characterData, hitPoint, LayerMask.GetMask("Player"));
        die = new Die(context, rb, health);

        canMove = new CanMove(transform, target.transform, 8f, 1f);
        canAttack = new CanAttack(this, transform, target.transform, characterData.HitRadius);

        actions = new List<CharacterAction>() {movement, attack, die};
        conditions = new List<CharacterCondition>() {canMove, canAttack};

    }

    void Update()
    {   
        RunConditions();
        
        movement.SetInput(canMove.ActionInput, canMove.direction.x);
        attack.SetInput(canAttack.ActionInput);
        

        selector.Execute(actions);
        Animation();
        DebugConsole();
    }
    void FixedUpdate()
    {
        selector.FixedExecute();
    }
    private void RunConditions()
    {
        foreach(CharacterCondition condition in conditions)
        {
            condition.CheckCondition();
        }
    }
    public void TriggerAttackAnimation()
    {
        attack.TriggerHitEvent();
    }
    public void StopAttackAnimation()
    {
        attack.StopAnimationEvent();
    }
    private void Animation()
    {
        animator.SetFloat("movement", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isAttacking", attack.isAttacking);
    }
    private void DebugConsole()
    {
        Debug.Log("attack input : " + canAttack.ActionInput);
    }

}
