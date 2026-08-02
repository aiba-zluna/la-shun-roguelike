using UnityEngine;

public class player_HP : MonoBehaviour
{

    private PlayerStats stats;
    private hp_Bar hpBar;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        stats.currentHealth = stats.maxHealth;

        hpBar = GetComponentInChildren<hp_Bar>();
    }


    public void TakeDamage(float damage)
    {
        
        stats.currentHealth -= damage;
        hpBar.UpdateHealth(stats.currentHealth, stats.maxHealth);

        if(stats.currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        stats.currentHealth += amount;
        stats.currentHealth = Mathf.Min(stats.currentHealth, stats.maxHealth);
    }


    void Die()
    {
        Debug.Log("Player Died");
        Destroy(gameObject);
    }
}
