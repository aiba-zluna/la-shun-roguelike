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
    [Tooltip("Healer also use this value for heal amount")]
    public float damage;
    [Tooltip("Higher Value = Higher Range")]
    public float attackRange;
    [Tooltip("Healer also uses this value for heal cast time")]
    public float attackSpeed;
    public float projectileSpeed = 8f;
    public float projectileLifetime = 5f;

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