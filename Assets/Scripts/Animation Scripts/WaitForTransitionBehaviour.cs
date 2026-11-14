
using UnityEngine;

public class WaitForTransitionBehaviour : StateMachineBehaviour
{
    private bool hasExecuted;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        hasExecuted = false;
    }

    public override void OnStateUpdate(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        if (hasExecuted)
            return;

        if (animator.IsInTransition(layerIndex))
            return;

        hasExecuted = true;

        // Jalankan fungsi setelah transition selesai
    }
}
