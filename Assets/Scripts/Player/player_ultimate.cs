using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class player_ultimate : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private CooldownUI cooldownUI;
    private PlayerStats stats;
    
    [SerializeField] private Transform firePointLeft;
    [SerializeField] private Transform firePointRight;
    private bool useLeftFirePoint = true;

    private PlayerShooting shooting;
    private InputSystem_Actions input;
    private PlayerMovement movement;

    private float fireTimer;
    private bool ultimateActive;
    private bool ultimateReady = true;

    void Awake()
    {
        input = new InputSystem_Actions();
        shooting = transform.parent.GetComponentInChildren<PlayerShooting>();
        movement = transform.GetComponentInParent<PlayerMovement>();
        stats = GetComponentInParent<PlayerStats>();
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Ultimate.performed += ultimate;
    }

    void OnDisable()
    {
        input.Player.Disable();
        input.Player.Ultimate.performed -= ultimate;
    }

    void ultimate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Ultimate Activated!");
            StartCoroutine(ultimateActivated());
        }
    }

    IEnumerator ultimateActivated()
    {
        if (!ultimateReady)
            yield break;

        shooting.canShoot = false;
        shooting.SetRotationLocked(true);
        movement.SetDirectionLocked(true);

        ultimateReady = false;
        ultimateActive = true;

        transform.rotation = shooting.transform.rotation;

        float activeTimer = 0f;
        fireTimer = 0f;

        while (activeTimer < stats.ultimateDuration)
        {
            activeTimer += Time.deltaTime;
            fireTimer += Time.deltaTime;

            if (fireTimer >= stats.ultimateFirerate)
            {
                if (useLeftFirePoint)
                {
                    shooting.Shoot(
                        bulletPrefab,
                        firePointLeft,
                        stats.ultimateDamage,
                        stats.ultimateSpeed,
                        stats.ultimateLifetime);
                }
                else
                {
                    shooting.Shoot(
                        bulletPrefab,
                        firePointRight,
                        stats.ultimateDamage,
                        stats.ultimateSpeed,
                        stats.ultimateLifetime);
                }
                useLeftFirePoint = !useLeftFirePoint;

                fireTimer = 0f;
            }

            yield return null;
        }

        shooting.canShoot = true;
        shooting.SetRotationLocked(false);
        movement.SetDirectionLocked(false);

        cooldownUI.StartCooldown(stats.ultimateCooldown);
        ultimateActive = false;

        yield return new WaitForSeconds(stats.ultimateCooldown);

        ultimateReady = true;

    }

    void Update()
    {
        if (!ultimateActive)
        {
            transform.rotation = shooting.transform.rotation;
        }
    }

}



