using UnityEngine;

public class EnemyMobRanged : EnemyBehavior
{
    [Header("Projectile")]
    [SerializeField] private EnemyProjectile projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Obstacle Detection")]
    [SerializeField] private LayerMask obstacleLayer;

    protected override void PerformAttack()
    {
        if (currentTarget == null)
            return;

        if (!HasLineOfSight())
            return;

        FireProjectile();
    }

    private bool HasLineOfSight()
    {
        Vector2 origin = firePoint != null
            ? firePoint.position
            : transform.position;

        Vector2 targetPos = currentTarget.position;
        Vector2 direction = (targetPos - origin).normalized;
        float distance = Vector2.Distance(origin, targetPos);

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            direction,
            distance,
            obstacleLayer);

        return hit.collider == null;
    }

    private void FireProjectile()
    {
        Vector2 origin = firePoint != null
            ? firePoint.position
            : transform.position;

        Vector2 direction = ((Vector2)currentTarget.position - origin).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        EnemyProjectile projectile = Instantiate(
            projectilePrefab,
            origin,
            Quaternion.Euler(0f, 0f, angle));

        projectile.Initialize(
            direction,
            stats.damage,
            stats.projectileSpeed,
            stats.projectileLifetime);
    }
}