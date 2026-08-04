using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Roaming,
        Chasing,
        Attacking
    }

    private EnemyState currentState;

    private AIPath aiPath;
    private AIDestinationSetter destinationSetter;

    private EnemyStats stats;
    private EnemyMobMelee mobMelee;

    private Transform player;

    private Vector2 spawnPoint;
    private Vector2 roamPoint;

    private float roamRadius;
    private float roamDelay;
    private float roamTimer;

    void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();

        stats = GetComponent<EnemyStats>();
        mobMelee = GetComponent<EnemyMobMelee>();

        spawnPoint = transform.position;

        roamRadius = stats.roamRadius;
        roamDelay = stats.roamDelay;

        ChangeState(EnemyState.Roaming);

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

                if (player == null)
                {
                    PlayerLost();
                    break;
                }

                float chaseDistance = Vector2.Distance(transform.position, player.position);

                if (chaseDistance <= stats.attackRange)
                {
                    ChangeState(EnemyState.Attacking);
                }

                break;

            case EnemyState.Attacking:

                if (player == null)
                {
                    PlayerLost();
                    break;
                }

                float attackDistance = Vector2.Distance(transform.position, player.position);

                if (attackDistance > stats.attackRange)
                {
                    ChangeState(EnemyState.Chasing);
                    break;
                }

                mobMelee.Attack(player);

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

    public void PlayerDetected(Transform playerTransform)
    {
        player = playerTransform;

        destinationSetter.target = playerTransform;

        ChangeState(EnemyState.Chasing);
    }

    public void PlayerLost()
    {
        player = null;

        roamTimer = 0f;

        ChangeState(EnemyState.Roaming);

        PickNewRoamPoint();
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState == newState)
            return;

        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Roaming:
            case EnemyState.Chasing:
                aiPath.canMove = true;
                break;

            case EnemyState.Attacking:
                aiPath.canMove = false;
                break;
        }
    }
}