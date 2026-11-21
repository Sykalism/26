using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public CharacterBehaviour character;
    public Image fillImage;
    public Image delayImage;
    public float delaySpeed = 2f;

    private Health health => character.stats.health;

    void Update()
    {
        SetBarFill();
        SetBarDelay();
    }
    private void SetBarFill()
    {
        fillImage.fillAmount = health.current / health.max;
    }
    private void SetBarDelay()
    {
        delayImage.fillAmount = Mathf.Lerp(delayImage.fillAmount, fillImage.fillAmount, delaySpeed * Time.deltaTime);
    }


}
