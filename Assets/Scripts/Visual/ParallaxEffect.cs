using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [Range(-1, 1)] public float XFactor;
    [Range(-1, 1)] public float YFactor;

    private Vector3 startPos;
    private Transform camPos;
    void Start()
    {
        startPos = transform.position;
        camPos = Camera.main.transform;
    }
    void FixedUpdate()
    {
        float Xtarget = camPos.position.x * XFactor;
        float Ytarget = camPos.position.y * YFactor;
        transform.position = new Vector3(startPos.x + Xtarget, startPos.y + Ytarget, 0f);
    }
}
