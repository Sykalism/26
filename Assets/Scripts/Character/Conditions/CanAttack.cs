using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class CanAttack : CharacterCondition
{
    private MonoBehaviour monoBehaviour;
    private Transform self;
    private Transform target;
    private float radius;
    private bool action;
    private bool input;
    
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

        if (range <= radius)
        {
            input = true;
        }
        if (input)
        {
            input = false;
            monoBehaviour.StartCoroutine(ResetNextFrame());
        }
        return action;
    }
    private IEnumerator ResetNextFrame()
    {
        action = true;
        yield return null;
        action = false;
    }
}
