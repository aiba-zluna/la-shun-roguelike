using UnityEngine;

public class player_HP : MonoBehaviour
{

    private PlayerMovement playerController;
    private PlayerStats stats;
    private hp_Bar hpBar;

    void Awake()
    {
        playerController = GetComponent<PlayerMovement>();
        stats = GetComponent<PlayerStats>();
        stats.currentHealth = stats.maxHealth;

        hpBar = GetComponentInChildren<hp_Bar>();
    }


    public bool TakeDamage(float damage)
    {
        if (playerController.DashCheck()) return false;

        stats.currentHealth -= damage;
        stats.currentHealth = Mathf.Max(stats.currentHealth, 0);

        hpBar.UpdateHealth(stats.currentHealth, stats.maxHealth); //update hp bar sprite

        if(stats.currentHealth <= 0)
        {
            Die();
        }
        return true;
    }

    public void Heal(float amount)
    {
        stats.currentHealth += amount;
        stats.currentHealth = Mathf.Min(stats.currentHealth, stats.maxHealth);
    }


    //temporary death
    void Die()
    {
        Destroy(gameObject);
    }
}
