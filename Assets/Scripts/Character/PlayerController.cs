using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject particleDash;
    [SerializeField] Transform checkpoint;
    

    //stats
    private Animator animator;
    private CharacterBehaviour CBehaviour;

    //Inputs
    private float inputDirection => Input.GetAxis("Horizontal"); 
    private bool moveInput => inputDirection != 0f;
    private bool runInput => Input.GetKey(KeyCode.LeftShift);
    private bool jumpInput => Input.GetKeyDown(KeyCode.Z);
    private bool dashInput => Input.GetKeyDown(KeyCode.C);
    private bool attackInput => Input.GetKeyDown(KeyCode.X);
 


    void Awake()
    {
        animator = GetComponent<Animator>();
        CBehaviour = GetComponent<CharacterBehaviour>();
    }
    void Update()
    {
        CBehaviour.actionInput.InitiateInput
        (
            inputDirection, 
            moveInput,
            runInput,
            jumpInput,
            dashInput,
            attackInput
        );
        Animation();

        Debug.Log(CBehaviour.healthPoint.CurrentHealth);

    }
    private void Animation()
    {
        if (CBehaviour.die.isDead)
        {
            animator.SetTrigger("isDead");
        }
        else
        {
            animator.SetFloat("move", Mathf.Abs(inputDirection));
            animator.SetFloat("verticalForce", CBehaviour.rb.linearVelocity.y);
            animator.SetInteger("attacks", CBehaviour.attack.currentAttack);
            animator.SetBool("isLanding", CBehaviour.isLanding);
            animator.SetBool("isGrounded", CBehaviour.isGrounded);
            animator.SetBool("isRunning", CBehaviour.movement.isRunning);
            animator.SetBool("isAttacking", CBehaviour.attack.isAttacking);
            animator.SetBool("isComboWindow", CBehaviour.attack.isComboWindow);
            if (CBehaviour.jump.IsExecuting)
            {
                animator.SetTrigger("jump");
            }
            if (CBehaviour.dash.StartAction) animator.SetTrigger("dash");
        }
        
    }
    private void SpawnParticleDash()
    {
        Instantiate
        (
            particleDash, 
            checkpoint.position, 
            checkpoint.rotation
        );
    }
}
