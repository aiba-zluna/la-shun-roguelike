using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour
{
    protected EnemyStats stats;
    protected Transform currentTarget;

    private float nextAttackTime;

    /// <summary>
    /// Should this enemy stop moving while attacking?
    /// </summary>
    public virtual bool StopMovementWhileAttacking => true;

    protected virtual void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    //====================================================
    // TARGET
    //====================================================

    public virtual Transform GetTarget()
    {
        return currentTarget;
    }

    //this is for for normal mob aggro
    protected virtual void SetTarget(Transform target)
    {
        currentTarget = target;
    }

    //this is for healer targeting, which targets other enemies
    public virtual void SetExternalTarget(Transform target)
    {
        SetTarget(target);
    }

    public virtual void OnPlayerEnterAggro(Transform player)
    {
        SetTarget(player);
    }

    public virtual void OnPlayerExitAggro(Transform player)
    {
        if (currentTarget == player)
            SetTarget(null);
    }

    //====================================================
    // ATTACK
    //====================================================

    public virtual bool InAttackRange()
    {
        if (currentTarget == null)
            return false;

        return Vector2.Distance(transform.position, currentTarget.position)
            <= stats.attackRange;
    }

    public virtual void Attack()
    {
        if (currentTarget == null)
            return;

        if (!CanAttack())
            return;

        PerformAttack();

        ResetCooldown();
    }

    protected virtual bool CanAttack()
    {
        return Time.time >= nextAttackTime;
    }

    protected void ResetCooldown()
    {
        nextAttackTime = Time.time + (1f / stats.attackSpeed);
    }

    /// <summary>
    /// Every enemy implements its own attack.
    /// </summary>
    protected abstract void PerformAttack();
}