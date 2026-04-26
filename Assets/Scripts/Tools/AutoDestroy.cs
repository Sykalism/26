using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
