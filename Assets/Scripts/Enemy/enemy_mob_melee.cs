using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private EnemyStats stats;

    private void Awake()
    {
        stats = GetComponent<EnemyStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player_HP player = collision.gameObject.GetComponent<player_HP>();

        if (player != null)
        {
            player.TakeDamage(stats.damage);
        }
    }
}