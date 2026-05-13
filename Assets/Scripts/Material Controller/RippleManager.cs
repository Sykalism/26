using System.Collections.Generic;
using UnityEngine;

public class RippleManager : MonoBehaviour
{
    public RenderTexture rippleRT;
    public Material decayMat;
    public Material drawMat;
    public Transform waterTransform;
    public Transform effector;
    public float rippleStrenght = 0.05f;

    public float decay = 0.98f;

    private RenderTexture temp;
    public Vector2 waterSize;
    private List<RippleData> ripples = new List<RippleData>();

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


       Graphics.Blit(Texture2D.grayTexture, rippleRT);
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            world.z = 0f;

            AddRipple(world);
        }

        for(int i = ripples.Count - 1; i >= 0; i--)
        {
            RippleData ripple = ripples[i];

            ripple.time += Time.deltaTime;

            drawMat.SetVector("_Center", new Vector4(ripple.uv.x, ripple.uv.y, 0, 0));
            drawMat.SetFloat("_RippleTime", ripple.time);
            drawMat.SetFloat("_RippleStrenght", rippleStrenght);

            Graphics.Blit(rippleRT, temp, drawMat);
            Graphics.Blit(temp, rippleRT);

            if (ripple.time > 1.5f)
            {
                ripples.RemoveAt(i);
            }
            Debug.Log(i);
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

        float u = localPos.x + 0.5f;
        float v = localPos.y + 0.5f;

        return new Vector2(u, v);
    }
    public void AddRipple(Vector3 worldPos)
    {
        Vector3 uv = WorldToUV(worldPos);

        RippleData ripple = new RippleData();

        ripple.uv = uv;
        ripple.time = 0f;

        ripples.Add(ripple);
    }
}
[System.Serializable]
public class RippleData
{
    public Vector2 uv;
    public float time;
}
