using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Movement")]
    public float moveSpeed;
    public float aggroRange;
    [Tooltip("Enemy Roam area with their spawn point as center")]
    public float roamRadius;
      [Tooltip("After enemy arrives in the random roam area, he stops usings this delay var")]
    public float roamDelay;

    [Header("Combat")]
    public float damage;
    [Tooltip("Higher Value = Higher Ranger")]
    public float attackRange;
    [Tooltip("Lower Value = Higher Attack Speed ")]
    public float attackCooldown;

    [Header("Drops")]
    [Tooltip("This shouldn't be changed")]
    public float keyDrop;
    [Tooltip("This shouldn't be changed")]
    public float buffDrop;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}