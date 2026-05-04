using UnityEngine;

public class RippleTrigger : MonoBehaviour
{
    [SerializeField] Transform trigger;
    [SerializeField] Material waterMat;

    public void SpawnRipple()
    {
        waterMat.SetVector("_ObjectPos", trigger.position);
        waterMat.SetFloat("_Spawn_Time", Time.time);
    }
}
