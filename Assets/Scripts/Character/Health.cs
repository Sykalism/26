using System.Collections;
using UnityEngine;

public class Health : CharacterStats
{
    public bool IsTakingDamage {get; private set;}
    public bool Hitted {get; private set;}
    public Vector2 affectedDamage {get; private set;}
    private CharacterBehaviour character;
    public LayerMask damageOrigin;
    public Health(float maxValue, CharacterBehaviour character) : base(maxValue)
    {
        this.character = character;
    }
    private IEnumerator NextFrame(System.Action<bool> callback)
    {
        callback?.Invoke(true);
        yield return null;
        callback?.Invoke(false);
    }
    public void TakeDamage(float value, LayerMask origin)
    {
        if (character.parry.IsExecuting && (int)character.directional.level == affectedDamage.x)
        {
            character.StartCoroutine(NextFrame(value => Hitted = value));
            character.parry.Parried();
            return;
        }
        Remove(value);
        damageOrigin = origin;
        character.StartCoroutine(NextFrame(value => IsTakingDamage = value));

    }
    private bool IsColliding(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & damageOrigin) != 0;
    }
    public void CheckAffectedDamage(Collider2D collision)
    {
        if (IsColliding(collision))
        {
            Vector2 dir = (collision.transform.position - character.transform.position).normalized;

            affectedDamage = new Vector2(
                Mathf.Sign(dir.x),
                Mathf.Sign(dir.y)
            );
            Debug.Log(affectedDamage);
            
        }
    }
    public void ResetAffectedDamage(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Weapon"))
        {
            affectedDamage = Vector2.zero;
        }
    }
 
}
