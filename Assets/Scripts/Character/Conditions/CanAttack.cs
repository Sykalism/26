using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Analytics;

public class CanAttack : CharacterCondition
{
    private MonoBehaviour monoBehaviour;
    private Transform self;
    private Transform target;
    private float radius;
    private bool input;
    private bool inputHasTriggred;
    private bool onRange;
    
    public CanAttack(MonoBehaviour monoBehaviour, Transform self, Transform target, float radius)
    {
        this.monoBehaviour = monoBehaviour;
        this.self = self;
        this.target = target;
        this.radius = radius;
    }
    public override void CheckCondition()
    {
        float range = Vector2.Distance(self.position, target.position);
        onRange = range <= radius;

        if (onRange)
        {
            if (inputHasTriggred) return;
            else monoBehaviour.StartCoroutine(NextFrame());
        }
        else inputHasTriggred = false;     
    }
    private IEnumerator NextFrame()
    {
        ActionInput = true;
        yield return null;
        ActionInput = false;

        inputHasTriggred = onRange;
    }

}
