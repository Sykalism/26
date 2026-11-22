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
    Endurance,
    RunSpeed, 
    JumpForce, 
    HeightForLanding, 
    LandingDelay, 
    HitRadius, 
    comboWindowTime,
    DashForce,
    DashDuration,
    KnockbackForce,
    KnockbackDuration,
    ParryCost;
    
    public int AttackVariant;
}
