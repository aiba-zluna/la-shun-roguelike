using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Combat")]
    public float damage;
    public float attackRange;
    public float attackCooldown;

    [Header("Drops")]
    public float keyDrop;
    public float buffDrop;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
}