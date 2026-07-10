using UnityEngine;

public class GamepadInput : MonoBehaviour
{
    void Update()
    {
       foreach( string joystick in Input.GetJoystickNames())
        {
            Debug.Log(joystick);
        } 
    }
}
