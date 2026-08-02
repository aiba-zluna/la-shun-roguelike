using UnityEngine;

public class enemy_HP : MonoBehaviour
{

    private EnemyStats stats;
    private bool isDead = false;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }


    public void TakeDamage(float damage)
    {
        stats.currentHealth -= damage;
        stats.currentHealth = Mathf.Max(stats.currentHealth, 0);

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


    //temporary death
    void Die()
    {
        if (isDead)
        return;

        isDead = true;

        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
