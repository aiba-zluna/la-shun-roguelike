using UnityEngine;
using Pathfinding;

public class EnemyMovement : MonoBehaviour
{
    private EnemyStats stats;
    private AIPath aiPath;

    void Awake()
    {
        stats = GetComponent<EnemyStats>();
        aiPath = GetComponent<AIPath>();

        aiPath.maxSpeed = stats.moveSpeed;
    }

    public void UpdateMoveSpeed()
    {
        aiPath.maxSpeed = stats.moveSpeed;
    }
}