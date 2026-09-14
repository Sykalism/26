using System;
using UnityEngine;

public class AI_MoveCondition : CharacterCondition
{
    private Transform self, target, headPoint;
    private float range, deadRange;
    public Vector3 Direction {get; private set;}
    public AI_MoveCondition(AIController ai, float range, float deadRange)
    {
        self = ai.transform;
        target = ai.target;
        headPoint = ai.headPoint;

        this.range = range;
        this.deadRange = deadRange;
    }
    public override void CheckCondition()
    {
        float distance = Vector3.Distance(self.position, target.position);
        Vector2 dir = target.position - self.position;
        Direction = new Vector2(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));

        if (distance <= range && distance > deadRange && PathCheck())
        {
            ActionInput = true;
        }
        else ActionInput = false;
        
    }
    private bool PathCheck()
    {
        return Physics2D.Raycast
        (
            headPoint.position, 
            new Vector2(Direction.x, -1), 
            5f,
            LayerMask.GetMask("Environment")
        );
    }
}
