using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public HealthPoint health;
    public Image fillImage;
    public Image delayImage;
    public float delaySpeed = 2f;

    void Update()
    {
        SetBarFill();
        SetBarDelay();
    }
    private void SetBarFill()
    {
        fillImage.fillAmount = health.CurrentHealth / health.MaxHealth;
    }
    private void SetBarDelay()
    {
        delayImage.fillAmount = Mathf.Lerp(delayImage.fillAmount, fillImage.fillAmount, delaySpeed * Time.deltaTime);
    }


}
