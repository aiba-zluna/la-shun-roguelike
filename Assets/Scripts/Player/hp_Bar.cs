using UnityEngine;
using UnityEngine.UI;

public class hp_Bar : MonoBehaviour
{
    [SerializeField] private Image fillImage;


    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        float percent = currentHealth / maxHealth;

        fillImage.fillAmount = percent;

        if (percent > 0.66f)
        {
            fillImage.color = Color.green;
        }
        else if (percent > 0.33f)
        {
            fillImage.color = Color.yellow;
        }
        else
        {
            fillImage.color = Color.red;
        }
    }
}