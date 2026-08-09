using UnityEngine;

public class HealerTargeting : MonoBehaviour
{
    [Header("Targeting")]
    [SerializeField] private float searchRange;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float searchInterval = 0.25f;

    private EnemyBehavior behavior;
    private EnemyStats currentTarget;

    private float nextSearchTime;

    private void Awake()
    {
        behavior = GetComponentInParent<EnemyBehavior>();

        EnemyStats stats = GetComponentInParent<EnemyStats>();

        searchRange = stats.aggroRange;

        if (behavior == null)
        {
            Debug.LogError($"{name}: EnemyBehavior not found in parent.");
        }
    }

    private void Update()
    {
        EnemyMobHealer healer = GetComponentInParent<EnemyMobHealer>();

        // Don't change targets while casting.
        if (healer != null && healer.IsCasting)
            return;

        if (Time.time < nextSearchTime)
            return;

        nextSearchTime = Time.time + searchInterval;

        FindLowestHealthEnemy();
    }

    private void FindLowestHealthEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            transform.position,
            searchRange,
            enemyLayer
        );

        EnemyStats lowestHealthEnemy = null;
        float lowestHealthPercent = 1f;

        foreach (Collider2D enemyCollider in enemies)
        {
            EnemyStats enemy = enemyCollider.GetComponentInParent<EnemyStats>();

            if (enemy == null)
                continue;

            // Don't target the healer itself
            if (enemy == GetComponentInParent<EnemyStats>())
                continue;

            // Ignore enemies that are already at full HP
            if (enemy.currentHealth >= enemy.maxHealth)
                continue;

            float healthPercent =
                (float)enemy.currentHealth / enemy.maxHealth;

            if (healthPercent < lowestHealthPercent)
            {
                lowestHealthPercent = healthPercent;
                lowestHealthEnemy = enemy;
            }
        }

        if (lowestHealthEnemy != null)
        {
            SetTarget(lowestHealthEnemy);
        }
        else
        {
            ClearTarget();
        }
    }

    private void SetTarget(EnemyStats target)
    {
        if (currentTarget == target)
            return;

        currentTarget = target;

        behavior.SetExternalTarget(target.transform);
    }

    private void ClearTarget()
    {
        if (currentTarget == null)
            return;

        currentTarget = null;

        behavior.SetExternalTarget(null);
    }

    public EnemyStats GetTarget()
    {
        return currentTarget;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}