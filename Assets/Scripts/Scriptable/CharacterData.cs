using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Base Status")]
    public string CharacterName;
    public float 
    MaxHealth, 
    Damage, 
    MovementSpeed, 
    RunSpeed, 
    Acceleration,
    JumpForce, 
    HeightForLanding, 
    LandingDelay, 
    HitRadius, 
    comboWindowTime,
    DashForce,
    DashDuration;
    
    public int AttackVariant;
}
