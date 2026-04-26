using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float _currentHealth;
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
    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
    }
    public void TakeHeal(float amount)
    {
        CurrentHealth += amount;
    }
}
