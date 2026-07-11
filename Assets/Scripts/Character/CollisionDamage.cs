using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
   public LayerMask targets;
   public float damagePercentage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsColliding(collision))
        {
            HealthPoint targetHP = collision.gameObject.GetComponent<HealthPoint>();
            CharacterBehaviour character = collision.gameObject.GetComponent<CharacterBehaviour>();
            if (targetHP != null && character != null)
            {
                if (!character.dash.IsExecuting)
                {
                    float totalDamage = damagePercentage / 100 * targetHP.MaxHealth;
                    targetHP.TakeDamage(totalDamage, LayerMask.GetMask("Hit Collider"));
                }
            }
        }
    }

    private bool IsColliding(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & targets) != 0;
    }
}