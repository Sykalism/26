using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] CharacterData characterData;
    [SerializeField] CharacterBehaviour behaviour;
    [SerializeField] GameObject target;

    private Rigidbody2D rb;
    private Animator animator;
    private ActionSelector selector;
    private List<CharacterAction> actions;
    private ActionContext context;
    private HealthPoint health;

    //Actions
    private Movement movement;
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

        movement = new Movement(context, rb, transform, behaviour, characterData.MovementSpeed, 0f, 15f);
        die = new Die(context, rb, health);

        actions = new List<CharacterAction>() {movement, die};
    }

    void Update()
    {
        canMove = new CanMove(transform, target.transform, 8f, 4f);
        canAttack = new CanAttack(this, transform, target.transform, characterData.HitRadius);
        
        if (canMove != null) movement.SetInput(canMove.ActionInput(), canMove.direction.x);

        selector.Execute(actions);
        Animation();
        DebugConsole();
    }
    void FixedUpdate()
    {
        selector.FixedExecute();
    }
    void Animation()
    {
        animator.SetFloat("movement", Mathf.Abs(rb.linearVelocity.x));
    }
    void DebugConsole()
    {
        Debug.Log(canAttack.ActionInput());
    }

}
