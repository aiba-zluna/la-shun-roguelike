using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyAggro : MonoBehaviour
{
    private EnemyBehavior behavior;
    private Transform player;
    private CircleCollider2D aggroCollider;

    private void Awake()
    {
        behavior = GetComponentInParent<EnemyBehavior>();

        aggroCollider = GetComponent<CircleCollider2D>();

        EnemyStats stats = GetComponentInParent<EnemyStats>();

        aggroCollider.radius = stats.aggroRange;

        if (behavior == null)
        {
            Debug.LogError($"{name}: EnemyBehavior not found in parent.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        player = other.transform;
        behavior.OnPlayerEnterAggro(player);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (player == other.transform)
        {
            behavior.OnPlayerExitAggro(player);
            player = null;
        }
    }
}