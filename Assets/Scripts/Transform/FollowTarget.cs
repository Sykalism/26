using UnityEngine;

public enum UpdateExecute
{
    LateUpdate,
    Default,
    FixedUpdate
}
public class FollowTarget : MonoBehaviour
{
    [SerializeField] UpdateExecute updateExecute = UpdateExecute.Default;
    [SerializeField] Transform target;
    [SerializeField] float smoothTime;
    [SerializeField] Vector3 offset;
    
    private Vector3 velocity = Vector3.zero;
    private void Execute()
    {
        if (!target) return;

        transform.position = Vector3.SmoothDamp
        (
            transform.position, 
            target.position, 
            ref velocity, 
            smoothTime
        ) + offset;
    }
    private void LateUpdate()
    {
        if (updateExecute == UpdateExecute.LateUpdate)
        {
            Execute();
        }
    }
    private void Update()
    {
        if (updateExecute == UpdateExecute.Default)
        {
            Execute();
        }
    }
    private void FixedUpdate()
    {
        if (updateExecute == UpdateExecute.FixedUpdate)
        {
            Execute();
        }
    }
}
