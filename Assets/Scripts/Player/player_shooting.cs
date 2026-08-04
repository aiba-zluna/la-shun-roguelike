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
        stats = GetComponentInParent<PlayerStats>();
    }

    private void Update()
    {
        mouseRotation();

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
        Vector2 direction = firePoint.right;
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.Initialize(
            direction,
            stats.attackDamage,
            stats.bulletSpeed,
            stats.bulletLifetime
        );
    }

    void mouseRotation()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = (mousePos - firePoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}