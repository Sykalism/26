using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;
    [SerializeField] LayerMask targetHit;

    private bool trigger;
    private bool hit;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsColliding(collision) && trigger)
        {
            HealthPoint targetHP = collision.gameObject.GetComponent<HealthPoint>();
            if (targetHP != null)
            {
                targetHP.TakeDamage(damage);
                Debug.Log("hit");
                TriggerOff();
            }
        }
    }
    public void TriggerOn()
    {
        trigger = true;
    }
    public void TriggerOff()
    {
        trigger = false;
    }
    private bool IsColliding(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & targetHit) != 0;
    }
    
}
