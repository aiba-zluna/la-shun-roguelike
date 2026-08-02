using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    private PlayerStats stats;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float fireTimer;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButton(0) && fireTimer >= 1f / stats.attackSpeed)
        {
            fireTimer = 0f;
            StartCoroutine(BurstFire());
        }
    }

    IEnumerator BurstFire()
    {
        for (int i = 0; i < stats.bulletsPerShot; i++)
        {
            Shoot();

            if (i < stats.bulletsPerShot - 1)
                yield return new WaitForSeconds(stats.shotDelay);
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);


        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            rotation
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.Initialize(
            direction,
            stats.attackDamage,
            stats.bulletSpeed,
            stats.bulletLifetime
        );
    }
}