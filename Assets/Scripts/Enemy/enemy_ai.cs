using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
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
    private EnemyBehavior behavior;

    [Header("Roaming")]
    private float roamRadius;
    private float roamDelay;

    [Header("Stuck Detection")]
    [SerializeField] private float stuckTime = 2f;

    private float roamTimer;
    private float stuckTimer;

    private Transform roamTarget;
    private Vector2 spawnPosition;

    private void Awake()
    {
        aiPath = GetComponent<AIPath>();
        destinationSetter = GetComponent<AIDestinationSetter>();
        behavior = GetComponent<EnemyBehavior>();

        EnemyStats stats = GetComponent<EnemyStats>();

        roamRadius = stats.roamRadius;
        roamDelay = stats.roamDelay;

        GameObject roamTargetObject = new GameObject($"{gameObject.name}_RoamTarget");
        roamTargetObject.hideFlags = HideFlags.HideAndDontSave;
        roamTarget = roamTargetObject.transform;
    }

    private void Start()
    {
        spawnPosition = transform.position;

        ChangeState(EnemyState.Roaming);
        PickNewRoamPoint();
    }

    private void Update()
    {
        Transform target = behavior.GetTarget();

        switch (currentState)
        {
            case EnemyState.Roaming:

                HandleRoaming();

                if (target != null)
                {
                    ChangeState(EnemyState.Chasing);
                }

                break;

            case EnemyState.Chasing:

                if (target == null)
                {
                    ChangeState(EnemyState.Roaming);
                    break;
                }

                destinationSetter.target = target;

                if (behavior.InAttackRange())
                {
                    ChangeState(EnemyState.Attacking);
                }

                break;

            case EnemyState.Attacking:

                if (target == null)
                {
                    ChangeState(EnemyState.Roaming);
                    break;
                }

                destinationSetter.target = target;

                if (!behavior.InAttackRange())
                {
                    ChangeState(EnemyState.Chasing);
                    break;
                }

                behavior.Attack();

                break;
        }

        aiPath.canMove =
            currentState != EnemyState.Attacking ||
            !behavior.StopMovementWhileAttacking;
    }

    private void ChangeState(EnemyState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case EnemyState.Roaming:

                destinationSetter.target = roamTarget;

                roamTimer = 0f;
                stuckTimer = 0f;

                PickNewRoamPoint();

                break;

            case EnemyState.Chasing:
                break;

            case EnemyState.Attacking:
                break;
        }
    }

    private void HandleRoaming()
    {
        // Reached destination normally
        if (!aiPath.pathPending && aiPath.reachedEndOfPath)
        {
            roamTimer += Time.deltaTime;

            if (roamTimer >= roamDelay)
            {
                PickNewRoamPoint();

                roamTimer = 0f;
                stuckTimer = 0f;
            }
        }
        else
        {
            roamTimer = 0f;
        }

        // ---------- Stuck Detection ----------
        if (!aiPath.pathPending &&
            !aiPath.reachedEndOfPath &&
            aiPath.desiredVelocity.magnitude > 0.1f &&
            aiPath.velocity.magnitude < 0.05f)
        {
            stuckTimer += Time.deltaTime;

            if (stuckTimer >= stuckTime)
            {
                PickNewRoamPoint();
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }

    private void PickNewRoamPoint()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamRadius;
        roamTarget.position = spawnPosition + randomOffset;

        destinationSetter.target = roamTarget;
    }

    private void OnDestroy()
    {
        if (roamTarget != null)
        {
            Destroy(roamTarget.gameObject);
        }
    }
}