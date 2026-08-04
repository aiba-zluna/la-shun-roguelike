using UnityEngine;
using Pathfinding;

public class EnemyAggro : MonoBehaviour
{
    private AIPath aiPath;
    private EnemyStats stats;
    private AIDestinationSetter destinationSetter;
    private CircleCollider2D circle;

    void Start()
    {
        aiPath = GetComponentInParent<AIPath>();
        destinationSetter = GetComponentInParent<AIDestinationSetter>();
        stats = GetComponentInParent<EnemyStats>();
        circle = GetComponent<CircleCollider2D>();

        aiPath.canMove = false;
        circle.radius = stats.aggroRange;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            destinationSetter.target = other.transform;
            aiPath.canMove = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            destinationSetter.target = null;
            aiPath.canMove = false;
        }
    }


}