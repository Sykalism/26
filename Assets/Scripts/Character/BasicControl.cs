using UnityEngine;

public class BasicControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float direction {get; private set;}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
    }
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction * 6f, rb.linearVelocity.y);
    }

}
