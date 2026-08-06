using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float damage;
    private float speed;
    private Vector2 direction;

    [SerializeField] private LayerMask destroyLayers;

    public void Initialize(Vector2 dir, float dmg, float projectileSpeed, float lifeTime)
    {
        direction = dir.normalized;
        damage = dmg;
        speed = projectileSpeed;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player_HP player = other.GetComponent<player_HP>();

        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        if ((destroyLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }
    }
}