using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
   public LayerMask targets;
   public float damagePercentage = 10;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsColliding(collision))
        {
            Health targetHP = collision.gameObject.GetComponent<CharacterBehaviour>().stats.health;
            CharacterBehaviour character = collision.gameObject.GetComponent<CharacterBehaviour>();
            if (targetHP != null && character != null)
            {
                if (!character.dash.IsExecuting)
                {
                    float totalDamage = damagePercentage / 100 * targetHP.max;
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