using System.Collections;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float _currentHealth;
    public bool IsTakingDamage {get; private set;}
    public float CurrentHealth
    {
        get => _currentHealth;

        set
        {
            if (_currentHealth != value)
            {
                _currentHealth = value;
            }
        }
    }
    private void Start()
    {
        CurrentHealth = MaxHealth;
    }
    private IEnumerator NextFrame()
    {
        IsTakingDamage = true;
        yield return null;
        IsTakingDamage = false;
    }
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        StartCoroutine(NextFrame());
    }
    public void TakeHeal(float amount)
    {
        CurrentHealth += amount;
    }
}
