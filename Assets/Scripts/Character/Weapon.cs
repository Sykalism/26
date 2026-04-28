using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    public bool isAttacking;
    [SerializeField] LayerMask targetHit;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ( isAttacking && ((1 << collision.gameObject.layer) & targetHit) != 0)
        {
            Debug.Log("hit");
            HealthPoint targetHP = collision.gameObject.GetComponent<HealthPoint>();
            if (targetHP != null)
            {
                targetHP.TakeDamage(damage);
            }
        }
    }
}
