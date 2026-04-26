using UnityEngine;

public class DynamicCameraZoom : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float runSize;
    [SerializeField] float zoomSpeed = 5f;

    private float idleSize;
    private Rigidbody2D playerRb;

    void Start()
    {
        idleSize = Camera.main.orthographicSize;
        playerRb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float speed = Mathf.Abs(playerRb.linearVelocity.x);
        float t = Mathf.InverseLerp(0f, player.runSpeed, speed);

        float targetSize = Mathf.Lerp(idleSize, runSize, t);
        Camera.main.orthographicSize = Mathf.Lerp(
            Camera.main.orthographicSize,
            targetSize,
            Time.deltaTime * zoomSpeed
        );

    }
}
