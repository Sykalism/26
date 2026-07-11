using System;
using System.Collections;
using UnityEngine;

public class HealthPoint : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float _currentHealth;
    public bool IsTakingDamage {get; private set;}
    public Vector2 affectedDirection {get; private set;}
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
    public LayerMask damageOrigin;


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
    public void TakeDamage(float amount, LayerMask damageOrigin)
    {
        this.damageOrigin = damageOrigin;
        CurrentHealth -= amount;
        StartCoroutine(NextFrame());
    }
    public void TakeHeal(float amount)
    {
        CurrentHealth += amount;
    }
    private bool IsColliding(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & damageOrigin) != 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsColliding(collision))
        {
            Vector2 dir = (collision.transform.position - transform.position).normalized;

            affectedDirection = new Vector2(
                Math.Sign(dir.x),
                Mathf.Sign(dir.y)
            );
            Debug.Log(affectedDirection);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            affectedDirection = Vector2.zero;
        }
    }
 
}
