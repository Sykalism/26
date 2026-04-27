using UnityEngine;

public class Dissolve : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float fadeSpeed = 1f;
    public bool isActive = false;

    private float fadeTime;
    private bool isFading = false;
    
    void Start()
    {
        material.SetFloat("_Fade", 0f);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFading = true;
        }
        if (isFading)
        {
            if (!isActive)
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
            isActive = true;
        }
        material.SetFloat("_Fade", fadeTime);
    }
    private void FadeOut()
    {
        fadeTime -= Time.deltaTime * fadeSpeed;
        if (fadeTime < 0f)
        {
            fadeTime = 0f;
            isFading = false;
            isActive = false;
        }
        material.SetFloat("_Fade", fadeTime);
    }

}
