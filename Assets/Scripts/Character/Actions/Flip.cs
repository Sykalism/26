using UnityEngine;

public class Flip : CharacterAction
{
    public bool controlViaAnimation => character.controlFlipViaAnimation;
    public bool onAnimation {get; private set;}
    public bool animationStart {get; private set;}
    public override bool IsExecuting => isFlipping;
    private CharacterBehaviour character;
    private Rigidbody2D rb => character.rb;
    private float direction;
    private bool flipInput;
    private bool isFlipping;
    private Vector2 startScale;

    public Flip(ActionContext context, CharacterBehaviour character) : base(context, 6)
    {
        this.character = character;

        startScale = character.transform.localScale;
    }
    public void SetInput(float direction, bool flipInput)
    {
        this.direction = direction;
        this.flipInput = flipInput;
    }
    public void Run()
    {
        float flipX = direction < 0 ? startScale.x * -1 : startScale.x;
        Vector2 newScale = new Vector2(flipX, startScale.y);
        character.transform.localScale = newScale;
    }
    public void StopActionEvent()
    {
        isFlipping = false;
        onAnimation = false;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return flipInput && direction != 0;
    }
    public override void OnEnter()
    {
        isFlipping = true;
        if (controlViaAnimation)
        {
            actionContext.Lock();
            animationStart = true;
            onAnimation = true;
        }
    }
    public override void Execute()
    {
        if (controlViaAnimation)
        {
            animationStart = false;
        }
        else
        {
            Run();
            isFlipping = false;
        }
    }
    public override void OnExit()
    {
        if (controlViaAnimation)
        {
            actionContext.Unlock();
        }
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
    }

}
