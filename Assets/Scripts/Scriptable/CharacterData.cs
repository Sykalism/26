using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/CharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("Base Status")]
    public string CharacterName;
    public float MaxHealth, Damage, MovementSpeed, HitRadius, comboWindowTime;
    public int AttackVariant;
}
