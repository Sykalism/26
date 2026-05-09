using UnityEngine;

public class RippleTrigger : MonoBehaviour
{
    public RenderTexture rippleRT;
    public Material decayMat;
    public Material drawMat;
    public Material waterMat;
    public Transform waterTransform;
    public Transform effector;

    public float decay = 0.98f;

    private RenderTexture temp;
    public Vector2 waterSize;

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


       Graphics.Blit(Texture2D.blackTexture, rippleRT);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            world.z = 0f;

            Vector2 uv = WorldToUV(world);

            Debug.Log(uv);

            drawMat.SetVector("_Center", new Vector4(uv.x, uv.y, 0, 0));
            drawMat.SetFloat("_Radius", 0.03f);
            
            waterMat?.SetVector("_RippleCenter", uv);

            Graphics.Blit(rippleRT, temp, drawMat);
            Graphics.Blit(temp, rippleRT);
        }
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
    public void TestDraw()
    {
        
    drawMat.SetVector("_Center", new Vector4(0.5f, 0.5f, 0, 0));
    drawMat.SetFloat("_Radius", 0.1f);

    Graphics.Blit(rippleRT, temp, drawMat);
    Graphics.Blit(temp, rippleRT);
    }
    public Vector2 WorldToUV(Vector3 worldPos)
    {
        Vector3 localPos = waterTransform.InverseTransformPoint(worldPos);

        Debug.Log (localPos);
        float u = localPos.x + 0.5f;
        float v = localPos.y + 0.5f;

        return new Vector2(u, v);
    }
}
