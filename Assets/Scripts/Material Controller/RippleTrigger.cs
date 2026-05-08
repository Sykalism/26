using UnityEngine;

public class RippleTrigger : MonoBehaviour
{
    public RenderTexture rippleRT;
    public Material decayMat;
    public Material drawMat;
    public Transform waterTransform;
    public Transform effector;

    public float decay = 0.98f;

    private RenderTexture temp;
    private Vector2 waterSize;

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

       waterSize = waterTransform.localScale;

       Graphics.Blit(Texture2D.whiteTexture, rippleRT);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = effector.position;

            world.z = 0f;

            Vector2 uv = WorldToUV(world);

            Debug.Log(uv);

            drawMat.SetVector("_Center", new Vector4(uv.x, uv.y, 0, 0));
            drawMat.SetVector("_Center", new Vector4(uv.x, uv.y, 0, 0));

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

        float u = (localPos.x / waterSize.x) + 0.5f;
        float v = (localPos.y / waterSize.y) + 0.5f;

        return new Vector2(u, v);
    }
}
