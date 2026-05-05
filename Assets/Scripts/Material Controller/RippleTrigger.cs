using UnityEngine;

public class RippleTrigger : MonoBehaviour
{
    [SerializeField] Transform trigger;
    [SerializeField] RenderTexture rippleRT;
    [SerializeField] Material decayMat;
    [SerializeField] Material waterMat;

    private RenderTexture temp;

    void Start()
    {
        temp = new RenderTexture(rippleRT.width, rippleRT.height, 0, rippleRT.format);

        Graphics.Blit(Texture2D.blackTexture, rippleRT);
    }
    void Update()
    {
        Graphics.Blit(rippleRT, temp, decayMat);
        Graphics.Blit(temp, rippleRT);
    }

    public void SpawnRipple()
    {
        if (waterMat == null) return;
        waterMat.SetVector("_ObjectPos", trigger.position);
        waterMat.SetFloat("_Spawn_Time", Time.time);
    }
}
