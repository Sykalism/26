using UnityEngine;

public class DebugAnimationEvent : MonoBehaviour
{
    private Animator animator;
    public void DebugConsole()
    {
        Debug.Log("func called");
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.Play("action");
        }
    }
}
