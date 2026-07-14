using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterBaseData : ScriptableObject
{
    [Header("Base Status")]
    public string CharacterName;
    public float 
    MaxHealth, 
    Damage, 
    MovementSpeed, 
    RunSpeed, 
    JumpForce, 
    HeightForLanding, 
    LandingDelay, 
    HitRadius, 
    comboWindowTime,
    DashForce,
    DashDuration,
    knockbackForce;
    
    public int AttackVariant;
}
