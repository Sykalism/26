using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] Transform target;
    private Animator animator;
    private List<CharacterCondition> conditions;
    private CharacterBehaviour CBehaviour;

    //Conditions
    private CanMove canMove;
    private CanAttack canAttack;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CBehaviour = GetComponent<CharacterBehaviour>();

        canMove = new CanMove(transform, target, 5f, 0.3f);
        canAttack = new CanAttack
        (
            this, 
            transform, 
            target,
            CBehaviour.characterData.HitRadius,
            CBehaviour.attack.isAttacking 
        );
    }
    void Start()
    {
        conditions = new List<CharacterCondition>() {canMove, canAttack};
    }
    void Update()
    {
        CBehaviour.actionInput.InitiateInput
        (
            canMove.direction.x,
            canMove.ActionInput,
            false,
            false,
            false,
            canAttack.ActionInput
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
