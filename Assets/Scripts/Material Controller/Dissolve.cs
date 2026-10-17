using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] [Range(0, 1)] float fade = 1f;

    void OnValidate()
    {
        material.SetFloat("_Fade", fade);
    }
    void Update()
    {
        material.SetFloat("_Fade", fade);
    }
}
