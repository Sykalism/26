using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    public int depth;

    private Collider2D collision;

    void Start()
    {
        collision = GetComponent<Collider2D>();
    }

    public void ToggleCollision(int currentDepth)
    {
        if (depth == currentDepth)
        {
            collision.excludeLayers = LayerMask.GetMask("Player");
        }
        else collision.excludeLayers = 0;
    }
    
}
