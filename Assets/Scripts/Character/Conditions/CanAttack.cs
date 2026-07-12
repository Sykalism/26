using System.Collections;
using UnityEngine;

public class CanAttack : CharacterCondition
{
    private MonoBehaviour monoBehaviour;
    private Transform self;
    private Transform target;
    private float radius;
    private bool inputHasTriggred;
    private bool onRange;
    private bool trigger;
    
    public CanAttack(MonoBehaviour monoBehaviour, Transform self, Transform target, float radius, bool trigger)
    {
        this.monoBehaviour = monoBehaviour;
        this.self = self;
        this.target = target;
        this.radius = radius;
        this.trigger = trigger;
    }
    public override void CheckCondition()
    {
        float range = Vector2.Distance(self.position, target.position);
        onRange = range <= radius;

        if (onRange)
        {
            if (inputHasTriggred) return;
            monoBehaviour.StartCoroutine(NextFrame());
        }
        else inputHasTriggred = false;     
    }
    private IEnumerator NextFrame()
    {
        ActionInput = true;
        yield return null;
        ActionInput = false;

        inputHasTriggred = trigger;
    }

}
