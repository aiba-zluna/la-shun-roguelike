using UnityEngine;

public class EnemyMobMelee : MonoBehaviour
{
    private EnemyStats enemyStats;
    private float nextAttackTime;

    void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

    public void Attack(Transform player)
    {
        if (Time.time < nextAttackTime)
            return;

        nextAttackTime = Time.time + enemyStats.attackCooldown;

        player_HP playerHP = player.GetComponent<player_HP>();

        if (playerHP != null)
        {
            playerHP.TakeDamage(enemyStats.damage);
        }
    }
}