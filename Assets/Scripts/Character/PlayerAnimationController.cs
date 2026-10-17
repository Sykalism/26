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
    private Animator weaponAnim;
    private DirectionalLevel visualDirection;

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
        weaponAnim = character.weaponObject.GetComponent<Animator>();
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
        Debug.Log(visualDirection);
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
            LeftSide();
        }
        if (directional.GetNewDirection() == DirectionalLevel.Right)
        {
            RightSide();
        }

    }
    private void RightSide()
    {
        sideSprite?.SetSprite("Right Side");
        obliqueSprite?.SetSprite("Right");
        visualDirection = DirectionalLevel.Right;
    }
    private void LeftSide()
    {
        sideSprite?.SetSprite("Left Side");
        obliqueSprite?.SetSprite("Left");
        visualDirection = DirectionalLevel.Left;
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
        Sheathe();
        animator.SetFloat("move", Mathf.Abs(character.movement.direction));
        animator.SetFloat("y force", character.rb.linearVelocity.y); 
        animator.SetInteger("attacks", character.attack.currentAttack);
        if (directional.isFlipped && character.isGrounded)
        {
            animator.SetTrigger("turning around");
            directional.SetNewDirection();
        }
        if (character.actionInput.attack) animator.SetTrigger("attack");
        if (character.actionInput.jump) animator.SetTrigger("jump");
        animator.SetBool("is grounded", character.isGrounded);
        animator.SetBool("is attacking", character.attack.IsExecuting);
        animator.SetBool("on combat window", character.attack.isComboWindow);
        animator.SetBool("readiness", character.attack.isComboWindow && character.movement.direction == 0);
        animator.SetBool("is landing", character.isLanding);
    }

    private void OpeningAttack()
    {
        if (!character.weapon.isDrawn)
        {
            weaponAnim.SetTrigger("draw");
            character.weapon.ToggleDrawnOn();
        }
    }
    private void Sheathe()
    {
        if (character.weapon.isDrawn && character.attack.isComboWindow)
        {
            if (character.attack.comboTimer.timeRemaining < character.data.comboWindowTime - 2.5 || 
                IsPlaying("idle"))
            {
                Debug.Log("woilah");
                weaponAnim.SetTrigger("sheathe");
                character.weapon.ToggleDrawnOff();
            }
        }
    }
    public bool IsPlaying(string clipName)
    {
        AnimatorClipInfo[] clips =
            animator.GetCurrentAnimatorClipInfo(0);

        foreach (var clip in clips)
        {
            if (clip.clip.name == clipName)
                return true;
        }

        return false;
    }

}
