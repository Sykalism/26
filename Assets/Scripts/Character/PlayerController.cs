using UnityEngine;

public class PlayerController : MonoBehaviour, ISaveable
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


    //get save and load data
    public void Save(SaveData data)
    {
        data.player.position = new float[2];
        data.player.position[0] = transform.position.x;
        data.player.position[1] = transform.position.y;

        data.player.currentHealth = CBehaviour.healthPoint.CurrentHealth;
    }
    public void Load(SaveData data)
    {
        transform.position = new Vector3
        (
            data.player.position[0],
            data.player.position[1],
            0f
        );

        CBehaviour.healthPoint.CurrentHealth = data.player.currentHealth;
    }
}
