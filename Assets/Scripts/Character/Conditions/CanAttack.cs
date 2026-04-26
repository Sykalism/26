using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class CanAttack : CharacterCondition
{
    private MonoBehaviour monoBehaviour;
    private Transform self;
    private Transform target;
    private float radius;
    private bool inputCondition;
    private bool input;
    private bool onRange;
    
    public CanAttack(MonoBehaviour monoBehaviour, Transform self, Transform target, float radius)
    {
        this.monoBehaviour = monoBehaviour;
        this.self = self;
        this.target = target;
        this.radius = radius;
    }
    public override bool ActionInput()
    {
        float range = Vector2.Distance(self.position, target.position);
        onRange = range <= radius;

        if (onRange)
        {
            inputCondition = true;
        }
        return false;
    }
    private IEnumerator NextFrame()
    {
        input = true;
        yield return null;
        input = false;
        
    }
}
