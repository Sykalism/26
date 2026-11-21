using System;
using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 100f;
    private float _currentHealth;
    public bool IsTakingDamage {get; private set;}
    public bool IsParried {get; private set;}
    public Vector2 affectedDamage {get; private set;}
    private CharacterBehaviour character;
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
        character = GetComponent<CharacterBehaviour>();
    }
    private IEnumerator NextFrame(System.Action<bool> callback)
    {
        callback?.Invoke(true);
        yield return null;
        callback?.Invoke(false);
    }
    public void TakeDamage(float amount, LayerMask damageOrigin)
    {
        if (character.parry.IsExecuting && (int)character.directional.level == affectedDamage.x)
        {
            StartCoroutine(NextFrame(value => IsParried = value));
            return;
        }

        this.damageOrigin = damageOrigin;
        CurrentHealth -= amount;
        StartCoroutine(NextFrame(value => IsTakingDamage = value));
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

            affectedDamage = new Vector2(
                Math.Sign(dir.x),
                Mathf.Sign(dir.y)
            );
            Debug.Log(affectedDamage);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            affectedDamage = Vector2.zero;
        }
    }
 
}
