using UnityEngine;
using NaughtyAttributes;

public class PlayerAnimationController : MonoBehaviour
{
    [Foldout("References")] [SerializeField] private GameObject oblique;
    [Foldout("References")] [SerializeField] private GameObject side;
    [Foldout("References")] [SerializeField] private CharacterBehaviour character;
    [Foldout("References")] [SerializeField] private DirectionalLevelControl directional;
    private CharacterSpriteController obliqueSprite;
    private CharacterSpriteController sideSprite;

    [Header("General")]
    [SerializeField] private bool isObliqued;

    private Animator animator;

    void OnValidate()
    {
        if (isObliqued) SetOblique();
        else SetSide();
    }
    void Awake()
    {
        obliqueSprite = oblique?.GetComponent<CharacterSpriteController>();
        sideSprite = side?.GetComponent<CharacterSpriteController>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Animation();
        if (!character.isGrounded)
        {
            character.autoFlip = true;
            if (directional.isFlipped)
            {
                ChangeVisualByDirection();
                directional.SetNewDirection();
            }
        }
        else character.autoFlip = false;
        
    }
    public void Flip()
    {
        character.movement.Flip();
        ChangeVisualByDirection();

    }
    private void ChangeVisualByDirection()
    {
        if (directional.GetNewDirection() == DirectionalLevel.Left)
        {
            sideSprite?.SetSprite("Left Side");
            obliqueSprite?.SetSprite("Left");
        }
        if (directional.GetNewDirection() == DirectionalLevel.Right)
        {
            sideSprite?.SetSprite("Right Side");
            obliqueSprite?.SetSprite("Right");
        }
    }
    public void SetOblique()
    {
        oblique.SetActive(true);
        side.SetActive(false);
    }
    public void SetSide()
    
    {
        oblique.SetActive(false);
        side.SetActive(true);
    }

    private void Animation()
    {
        animator.SetFloat("move direction", Mathf.Abs(character.movement.direction));
        animator.SetFloat("y force", character.rb.linearVelocity.y); 
        if (directional.isFlipped && character.isGrounded)
        {
            animator.SetTrigger("turning around");
            directional.SetNewDirection();
        }
        if (character.jump.IsExecuting) animator.SetTrigger("jump");
        animator.SetBool("is grounded", character.isGrounded);
    }

}
