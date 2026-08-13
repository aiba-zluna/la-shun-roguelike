using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] private Image img;

    private float timer;
    private float cooldown;
    private bool onCooldown;

    public void StartCooldown(float cooldownTime)
    {
        cooldown = cooldownTime;
        timer = 0f;
        onCooldown = true;

        img.fillAmount = 1f;
    }

    void Update()
    {
        if (!onCooldown)
            return;

        timer += Time.deltaTime;

        img.fillAmount = 1f - (timer / cooldown);

        if (timer >= cooldown)
        {
            timer = cooldown;
            img.fillAmount = 0f;
            onCooldown = false;
        }
    }
}
