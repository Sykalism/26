using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] CharacterBaseData characterData;
    [SerializeField] LayerMask targetHit;
    public bool isDrawn = true;

    private bool trigger;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (IsColliding(collision) && trigger)
        {
            HealthPoint targetHP = collision.gameObject.GetComponent<HealthPoint>();
            if (targetHP != null)
            {
                targetHP.TakeDamage(characterData.Damage, LayerMask.GetMask("Weapon"));
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
    public void ToggleDrawnOn()
    {
        isDrawn = true;
    }
    public void ToggleDrawnOff()
    {
        isDrawn = false;
    }
    private bool IsColliding(Collider2D collider)
    {
        return ((1 << collider.gameObject.layer) & targetHit) != 0;
    }
    
}
