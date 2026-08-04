using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Roaming,
        Chasing
    }

    [Header("Roaming")]
    [SerializeField] private float roamRadius;
    [SerializeField] private float roamDelay;

    private EnemyState currentState;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;

    private Vector2 spawnPoint;
    private Vector2 roamPoint;

    private float roamTimer;

    private EnemyStats stats;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        stats = GetComponent<EnemyStats>();

        spawnPoint = transform.position;

        currentState = EnemyState.Roaming;

        roamRadius = stats.roamRadius;
        roamDelay = stats.roamDelay;

        PickNewRoamPoint();
    }

    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Roaming:

                if (!aiPath.pathPending && aiPath.reachedEndOfPath)
                {
                    roamTimer += Time.deltaTime;

                    if (roamTimer >= roamDelay)
                    {
                        PickNewRoamPoint();
                        roamTimer = 0f;
                    }
                }

                break;

            case EnemyState.Chasing:
                break;
        }
    }

    void PickNewRoamPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamRadius;
        roamPoint = spawnPoint + randomOffset;

        destinationSetter.target = null;
        aiPath.destination = roamPoint;
    }

    public void PlayerDetected(Transform player)
    {
        currentState = EnemyState.Chasing;

        destinationSetter.target = player;
    }

    public void PlayerLost()
    {
        currentState = EnemyState.Roaming;

        PickNewRoamPoint();
    }
}