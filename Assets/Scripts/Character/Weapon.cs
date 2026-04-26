using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Material weaponMaterial;
    [SerializeField] float fadeSpeed = 1f;
    public bool isDrawing = false;
    public Transform hitPoint;
    public float damage = 20f;
    public float hitRadius = 1f;

    private float fadeTime;
    private bool isFading = false;
    
    void Start()
    {
        weaponMaterial.SetFloat("_Fade", 0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFading = true;
        }
        if (isFading)
        {
            if (!isDrawing)
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }

    }
    private void FadeIn()
    {
        fadeTime += Time.deltaTime * fadeSpeed;
        if (fadeTime > 1f)
        {
            fadeTime  = 1f;
            isFading = false;
            isDrawing = true;
        }
        weaponMaterial.SetFloat("_Fade", fadeTime);
    }
    private void FadeOut()
    {
        fadeTime -= Time.deltaTime * fadeSpeed;
        if (fadeTime < 0f)
        {
            fadeTime = 0f;
            isFading = false;
            isDrawing = false;
        }
        weaponMaterial.SetFloat("_Fade", fadeTime);
    }

}
