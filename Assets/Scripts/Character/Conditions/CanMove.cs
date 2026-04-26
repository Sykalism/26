using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CanMove : CharacterCondition
{
    private Transform self, target;
    private float range, deadRange;
    public Vector3 direction {get; private set;}
    public CanMove(Transform self, Transform target, float range, float deadRange)
    {
        this.self = self;
        this.target = target;
        this.range = range;
        this.deadRange = deadRange;
    }
    public override bool ActionInput()
    {
        float distance = Vector3.Distance(self.position, target.position);
        Vector2 dir = target.position - self.position;
        direction = new Vector2(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));

        if (distance <= range && distance > deadRange)
        {
            return true;
        }
        else return false;
    }
}
