using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform target;
    public Transform headPoint;
    private Animator animator;
    private List<CharacterCondition> conditions;
    private CharacterBehaviour CBehaviour;

    //Conditions
    private AI_MoveCondition moveCondition;
    private AI_AttackCondition attackCondition;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CBehaviour = GetComponent<CharacterBehaviour>();
    }
    void Start()
    {
        moveCondition = new AI_MoveCondition(this, 10f, 0.5f);
        attackCondition = new AI_AttackCondition
        (
            this, 
            transform, 
            target,
            CBehaviour.characterData.HitRadius,
            CBehaviour.attack.isAttacking 
        );
        
        conditions = new List<CharacterCondition>() {moveCondition, attackCondition};
    }
    void Update()
    {
        CBehaviour.actionInput.InitiateInput
        (
            moveCondition.Direction.x,
            moveCondition.ActionInput,
            false,
            false,
            false,
            attackCondition.ActionInput
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
        animator.SetFloat("movement", Mathf.Abs(CBehaviour.rb.linearVelocity.x));
        animator.SetInteger("attackVariation", CBehaviour.attack.currentAttack);
        animator.SetBool("isAttacking", CBehaviour.attack.isAttacking);
    }
    private void DebugConsole()
    {
    }

}
