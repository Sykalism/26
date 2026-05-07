using UnityEngine;

public class RippleTrigger : MonoBehaviour
{
    [SerializeField] Transform trigger;
    [SerializeField] Material waterMat;
    public RenderTexture rippleRT;
    public Material decayMat;
    public Material drawMat;
    public float decay = 0.98f;

    private RenderTexture temp;

    void Start()
    {
        temp = new RenderTexture(
            rippleRT.width,
            rippleRT.height,
            0,
            rippleRT.format
       );

       temp.Create();
       rippleRT.Create();

       Graphics.Blit(Texture2D.whiteTexture, rippleRT);
    }
    void Update()
    {
        decayMat.SetFloat("_Decay", decay);

        Graphics.Blit(rippleRT, temp, decayMat);
        Graphics.Blit(temp, rippleRT);

        if (Input.GetKeyDown(KeyCode.T))
        {
            TestDraw(); 
        }
    }

    private void OnDestroy()
    {
        if (temp != null)
        {
            temp.Release();
        }
    }


    public void SpawnRipple()
    {
        if (waterMat == null) return;
        waterMat.SetVector("_ObjectPos", trigger.position);
        waterMat.SetFloat("_Spawn_Time", Time.time);
    }
    public void TestDraw()
    {
        
    drawMat.SetVector("_Center", new Vector4(0.5f, 0.5f, 0, 0));
    drawMat.SetFloat("_Radius", 0.1f);

    Graphics.Blit(rippleRT, temp, drawMat);
    Graphics.Blit(temp, rippleRT);
    }
}
