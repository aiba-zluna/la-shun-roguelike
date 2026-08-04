using UnityEngine;
using Pathfinding;

public class EnemyAggro : MonoBehaviour
{
    private AIPath aiPath;
    private EnemyStats stats;
    private EnemyAI EnemyAI;
    private AIDestinationSetter destinationSetter;
    private CircleCollider2D circle;

    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        stats = GetComponentInParent<EnemyStats>();
        circle = GetComponent<CircleCollider2D>();
        EnemyAI = GetComponentInParent<EnemyAI>();

        //aiPath.canMove = false;
        circle.radius = stats.aggroRange;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAI.PlayerDetected(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            EnemyAI.PlayerLost();
        }
    }


}