using System.Collections.Generic;

public class ActionSelector
{
    private CharacterAction currentAction;
    public void Execute(List<CharacterAction> actions)
    {
        if (currentAction != null && currentAction.IsExecuting)
        {
            currentAction.Execute();
            return;
        }
        CharacterAction selected = null;
        foreach (var action in actions)
        {
            if (!action.Condition())
            {
                continue;
            }
            if (selected == null || action.priority > selected.priority)
            {
                selected = action;
            }
        }
        if (selected != currentAction)
        {
            currentAction?.OnExit();
            currentAction = selected;
            currentAction?.OnEnter();
        }
        currentAction?.Execute();
    }
    public void FixedExecute()
    {
        currentAction?.FixedExecute();
    }
}
