using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyStats stats;
    protected float nextAttackTime;

    protected virtual void Awake()
    {
        stats = GetComponent<EnemyStats>();

        if (stats == null)
        {
            Debug.LogError($"{name} is missing EnemyStats.");
        }
    }

    protected bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void ResetCooldown()
    {
        nextAttackTime = Time.time + stats.attackCooldown;
    }

    public abstract void Attack(Transform player);
}