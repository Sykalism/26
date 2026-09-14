using UnityEngine;

public class BoundsDebug : MonoBehaviour
{
    private Collider2D thisCollider;
    private Bounds box;


    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        
    }

    void Update()
    {
        box = thisCollider.bounds;
        Debug.Log(box.max.y);
    }
}
