using System;
using UnityEngine;

public class CharacterBehaviour
{
    public float heightToGround {get; private set;}
    public bool isLanding {get; private set;}
    public bool isGrounded {get; private set;}
    public bool isTouchingObstacle {get; private set;}
    private bool beLanding;
    private float timer;

    public void GroundCheck(Vector2 origin)
    {
        isGrounded = Physics2D.Raycast(origin, Vector2.down, 0.1f, LayerMask.GetMask("Environment"));
    }
    public void ObstacleCheck(Vector2 origin, Vector2 direction, float distance)
    {
        Vector2 offset = new Vector2(0f, 0.1f);
        isTouchingObstacle = Physics2D.Raycast(origin + offset, Vector2.right * Mathf.Sign(direction.x), distance, LayerMask.GetMask("Environment"));
    }
    public void Landing(Vector2 position, float height, float delay)
    {
        var ray = Physics2D.Raycast
        (
            position,
            Vector2.down,
            Mathf.Infinity
        );
        if (ray)
        {
            heightToGround = ray.distance;
        }

        if (heightToGround > height) beLanding = true;
        if (beLanding && heightToGround < 0.1f)
        {
            isLanding = true;
            beLanding = false;
        }
        if (isLanding)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                timer = 0f;
                isLanding = false;
            }
        }
    }
}
