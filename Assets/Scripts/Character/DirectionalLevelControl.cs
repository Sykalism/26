using UnityEngine;
public enum DirectionalLevel
{
    Left = -1,
    flipping = 0,
    Right = 1,
}
public class DirectionalLevelControl : MonoBehaviour
{
    //<<SUMMARY>> 
    // -pokoknya ada 'level direction' (arah saat ini) sama 'new direction' (arah selanjutnya)
    // -lier gusti

    [SerializeField] private bool autoSet;
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
                if (autoSet) SetNewDirection();
            }
            if (inputAxis < 0)
            {
                if (level == DirectionalLevel.Left) return;
                newDirection = -1;
                isFlipped = true;
                if (autoSet) SetNewDirection();
            }
        }
      
    }

    public void SetNewDirection()
    {
        level = (DirectionalLevel)newDirection;
        isFlipped = false;
    }
    public DirectionalLevel GetNewDirection()
    {
        return (DirectionalLevel)newDirection;
    }
}
