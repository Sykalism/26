public abstract class CharacterAction
{
    protected ActionContext actionContext;
    public int priority {get; protected set;}
    public virtual bool IsExecuting => false;
    public bool StartAction {get; protected set;}
    public CharacterAction(ActionContext context, int priority)
    {
        this.actionContext = context;
        this.priority = priority;

    }
    public virtual void OnEnter(){}
    public virtual void OnExit(){}
    public abstract bool Condition();
    public abstract void Execute();
    public virtual void FixedExecute(){}
}
public class ActionContext
{
    public bool isLocked {get; private set;}

    public void Lock()
    {
        isLocked = true;
    }
    public void Unlock()
    {
        isLocked = false;
    }
}
