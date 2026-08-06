using UnityEngine;

public class EnemyMobMelee : EnemyBehavior
{
    protected override void PerformAttack()
    {
        if (currentTarget == null)
            return;

        player_HP playerHP = currentTarget.GetComponent<player_HP>();

        if (playerHP == null)
            return;

        playerHP.TakeDamage(stats.damage);
    }
}