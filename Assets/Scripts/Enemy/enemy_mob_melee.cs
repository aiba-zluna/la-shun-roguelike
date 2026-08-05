using UnityEngine;

public class EnemyMobMelee : EnemyAttack
{
    public override void Attack(Transform player)
    {
        if (!CanAttack())
            return;

        player_HP hp = player.GetComponent<player_HP>();

        if (hp == null)
            return;

        hp.TakeDamage(stats.damage);

        ResetCooldown();
    }
}