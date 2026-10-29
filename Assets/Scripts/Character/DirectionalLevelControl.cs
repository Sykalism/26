using System.Collections;
using UnityEngine;
public enum DirectionalLevel
{
    Left = -1,
    flipping = 0,
    Right = 1,
}
public class DirectionalLevelControl
{
    //<<SUMMARY>> 
    // -pokoknya ada 'level direction' (arah saat ini) sama 'new direction' (arah selanjutnya)
    // -lier gusti

    public bool autoSet = true;

    public DirectionalLevel level {get; private set;}
    public bool flipLocked;
    private int newDirection;
    private CharacterBehaviour character;
    public bool isFlipped {get; private set;}

    public DirectionalLevelControl(CharacterBehaviour character)
    {
        this.character = character;
    }

    public void Control(float inputAxis)
    {
        if (inputAxis != 0)
        {
            if (inputAxis > 0)
            {
                if (level == DirectionalLevel.Right || flipLocked) return;
                newDirection = 1;
                character.StartCoroutine(NextFrame());
                if (autoSet) SetNewDirection();
            }
            if (inputAxis < 0)
            {
                if (level == DirectionalLevel.Left || flipLocked) return;
                newDirection = -1;
                character.StartCoroutine(NextFrame());
                if (autoSet) SetNewDirection();
            }
        }
      
    }
    private IEnumerator NextFrame()
    {
        isFlipped = true;
        yield return null;
        isFlipped = false;
    }
    public void SetNewDirection()
    {
        level = (DirectionalLevel)newDirection;
    }
    public DirectionalLevel GetNewDirection()
    {
        return (DirectionalLevel)newDirection;
    }
}
