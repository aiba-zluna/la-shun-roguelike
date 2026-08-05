using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage;
    private float speed;
    private Vector2 direction;
    [SerializeField] private LayerMask destroyLayers;

    public void Initialize(Vector2 dir, float dmg, float bulletSpeed, float lifeTime)
    {
        direction = dir;
        damage = dmg;
        speed = bulletSpeed;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHP enemy = other.GetComponent<EnemyHP>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if ((destroyLayers.value & (1 << other.gameObject.layer)) != 0)
        {
            Destroy(gameObject);
        }

    }
}