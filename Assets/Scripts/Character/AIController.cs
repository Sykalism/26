using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform target;
    public Transform headPoint;
    private Animator animator;
    private List<CharacterCondition> conditions;
    private CharacterBehaviour character;

    //Conditions
    private AI_MoveCondition moveCondition;
    private AI_AttackCondition attackCondition;

    void Awake()
    {
        animator = GetComponent<Animator>();
        character = GetComponent<CharacterBehaviour>();
    }
    void Start()
    {
        moveCondition = new AI_MoveCondition(this, 10f, 0.5f);
        attackCondition = new AI_AttackCondition
        (
            this, 
            transform, 
            target,
            character.data.HitRadius,
            character.attack.IsExecuting 
        );
        
        conditions = new List<CharacterCondition>() {moveCondition, attackCondition};
    }
    void Update()
    {
        character.actionInput.InitiateInput
        (
            moveCondition.Direction.x,
            moveCondition.ActionInput,
            false,
            false,
            false,
            attackCondition.ActionInput,
            character.directional.isFlipped
            
        );

        Animation();
        ExecuteConditions();
    }
    private void ExecuteConditions()
    {
        foreach(CharacterCondition condition in conditions)
        {
            condition.CheckCondition();
        }
    }


    private void Animation()
    {
        animator.SetFloat("movement", Mathf.Abs(character.rb.linearVelocity.x));
        animator.SetInteger("attackVariation", character.attack.currentAttack);
        animator.SetBool("isAttacking", character.attack.IsExecuting);
    }
    private void DebugConsole()
    {
    }

}
