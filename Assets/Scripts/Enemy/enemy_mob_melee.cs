using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player_HP player = collision.gameObject.GetComponent<player_HP>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}