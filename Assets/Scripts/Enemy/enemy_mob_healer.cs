using UnityEngine;

public class EnemyMobHealer : EnemyBehavior
{
    [Header("Healing")]
    [SerializeField] private float healingRadius = 2f;
    [SerializeField] private HealAoEVisual healAoEVisual;

    private bool isCasting;
    private float castTimer;

    private Transform castingTarget;

    /// <summary>
    /// Allows HealerTargeting to know whether the healer is currently casting.
    /// </summary>
    public bool IsCasting => isCasting;

    /// <summary>
    /// While casting, ignore normal attack range checks.
    /// This allows the target to move away while the cast continues.
    /// </summary>
    public override bool InAttackRange()
    {
        if (isCasting)
            return true;

        return base.InAttackRange();
    }

    protected override void PerformAttack()
    {
        // Don't start another cast while already casting.
        if (isCasting)
            return;

        if (currentTarget == null)
            return;

        StartHealingCast();
    }

    private void StartHealingCast()
    {
        isCasting = true;
        castTimer = 0f;

        // Lock the target for the duration of the cast.
        castingTarget = currentTarget;

        // Show the AoE and make it follow the locked target.
        if (healAoEVisual != null)
        {
            healAoEVisual.Show(castingTarget, healingRadius);
        }
    }

    private void Update()
    {
        if (!isCasting)
            return;

        // Target was destroyed during the cast.
        if (castingTarget == null)
        {
            CancelCast();
            return;
        }

        castTimer += Time.deltaTime;

        // Keep the visual AoE centered on the locked target.
        if (healAoEVisual != null)
        {
            healAoEVisual.UpdatePosition(
                castingTarget,
                healingRadius
            );
        }

        // Attack Speed directly represents cast time.
        // Example:
        // Attack Speed = 1 → 1 second
        // Attack Speed = 5 → 5 seconds
        float castTime = stats.attackSpeed;

        if (castTimer >= castTime)
        {
            CompleteHealingCast();
        }
    }

    private void CompleteHealingCast()
    {
        if (castingTarget == null)
        {
            CancelCast();
            return;
        }

        // Use the target's CURRENT position.
        // The AoE follows the target throughout the cast.
        Vector2 aoeCenter = castingTarget.position;

        Collider2D[] targets = Physics2D.OverlapCircleAll(
            aoeCenter,
            healingRadius
        );

        foreach (Collider2D collider in targets)
        {
            EnemyStats enemy = collider.GetComponentInParent<EnemyStats>();

            if (enemy == null)
                continue;

            // Don't heal the healer itself.
            if (enemy == stats)
                continue;

            // Only heal injured enemies.
            if (enemy.currentHealth >= enemy.maxHealth)
                continue;

            // Attack Damage determines the amount healed.
            enemy.currentHealth += stats.damage;

            // Prevent overhealing.
            if (enemy.currentHealth > enemy.maxHealth)
            {
                enemy.currentHealth = enemy.maxHealth;
            }
        }

        FinishCast();
    }

    private void CancelCast()
    {
        FinishCast();
    }

    private void FinishCast()
    {
        isCasting = false;
        castTimer = 0f;
        castingTarget = null;

        // Hide the AoE visual.
        if (healAoEVisual != null)
        {
            healAoEVisual.Hide();
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Shows the actual healing radius in the Unity Scene view.
        Gizmos.DrawWireSphere(transform.position, healingRadius);
    }
}
