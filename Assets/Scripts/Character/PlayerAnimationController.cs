using UnityEngine;
using NaughtyAttributes;
using System;
using NUnit.Framework;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour
{
    [Foldout("References")] [SerializeField] private GameObject oblique;
    [Foldout("References")] [SerializeField] private GameObject side;
    [Foldout("References")] [SerializeField] private PlayerController player;

    private CharacterBehaviour character => player.character;
    private CharacterSpriteController obliqueSprite;
    private CharacterSpriteController sideSprite;

    [Header("General")]
    [SerializeField] private bool isObliqued;

    private Animator animator;
    private Animator weaponAnim;
    private DirectionalLevelControl directional => character.directional;

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
    void Start()
    {
        weaponAnim = character.weaponObject.GetComponent<Animator>();
    }

    void Update()
    {
        Animation();    
    }
    private void Animation()
    {
        Sheathe();
        if (!player.autoFlip && directional.isFlipped)
        {
            animator.SetTrigger("turning around");
        }
        if (character.actionInput.jump) animator.SetTrigger("jump");
        animator.SetInteger("attacks", character.attack.currentAttack);
        animator.SetFloat("move", Math.Abs(character.rb.linearVelocity.x));
        animator.SetFloat("y force", character.rb.linearVelocity.y);
        animator.SetBool("is grounded", character.isGrounded);
        animator.SetBool("is dashing", character.dash.IsExecuting);
        animator.SetBool("is landing", character.isLanding);
        animator.SetBool("is attacking", character.attack.IsExecuting);
        animator.SetBool("readiness", character.attack.isComboWindow && character.movement.direction == 0);
        animator.SetBool("on combo window", character.attack.isComboWindow);
    }
    public void FlipEvent()
    {
        StartCoroutine(NextFrame());
        ChangeVisualByDirection();
    }
    private IEnumerator NextFrame()
    {
        player.SetFlipInput(true);
        yield return null;
        player.SetFlipInput(false);
    }
    private void ChangeVisualByDirection()
    {
        if (directional.GetNewDirection() == DirectionalLevel.Left)
        {
            VisualDirection("Left");
        }
        if (directional.GetNewDirection() == DirectionalLevel.Right)
        {
            VisualDirection("Right");
        }

    }
    private void VisualDirection(string direction)
    {
        sideSprite?.SetSprite(direction);
        obliqueSprite?.SetSprite(direction);
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
