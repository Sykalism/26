using UnityEngine;

public class Flip : CharacterAction
{
    private CharacterBehaviour character;
    private Rigidbody2D rb => character.rb;
    private float direction;
    private bool flipInput;
    private Vector2 startScale;
    private bool isFlipping;
    public override bool IsExecuting => isFlipping;

    public Flip(ActionContext context, CharacterBehaviour character) : base(context, 7)
    {
        this.character = character;

        startScale = character.transform.localScale;
    }
    public void SetInput(float direction, bool flipInput)
    {
        this.direction = direction;
        this.flipInput = flipInput;
    }
    public override bool Condition()
    {
        if (actionContext.isLocked) return false;
        return flipInput && direction != 0;
    }
    public override void Execute()
    {
        float flipX = direction < 0 ? startScale.x * -1 : startScale.x;
        Vector2 newScale = new Vector2(flipX, startScale.y);
        character.transform.localScale = newScale;
    }
    public override void FixedExecute()
    {
        rb.linearVelocity = Vector2.zero;
    }

}
