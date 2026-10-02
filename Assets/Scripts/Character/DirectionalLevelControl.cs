using UnityEngine;
public enum DirectionalLevel
{
    Left = -1,
    flipping = 0,
    Right = 1,
}
public class DirectionalLevelControl : MonoBehaviour
{
    [SerializeField] private bool autoFlip;
    private float inputAxis;
    public DirectionalLevel level {get; private set;} = DirectionalLevel.Right;
    private int newDirection;
    public bool isFlipped {get; private set;}
    void Start()
    {
        
    }
    void Update()
    {
        inputAxis = Input.GetAxis("Horizontal");
        Control();
    }
    private void Control()
    {
        if (inputAxis != 0)
        {
            if (inputAxis > 0)
            {
                if (level == DirectionalLevel.Right) return;
                newDirection = 1;
                isFlipped = true;
                Debug.Log(newDirection);
                if (autoFlip) SetNewDirection();
            }
            if (inputAxis < 0)
            {
                if (level == DirectionalLevel.Left) return;
                newDirection = -1;
                isFlipped = true;
                Debug.Log(newDirection);
                if (autoFlip) SetNewDirection();
            }
        }
      
    }

    public void SetNewDirection()
    {
        level = (DirectionalLevel)newDirection;
        isFlipped = false;
        Debug.Log(level);
    }
    public DirectionalLevel GetNewDirection()
    {
        return (DirectionalLevel)newDirection;
    }
}
