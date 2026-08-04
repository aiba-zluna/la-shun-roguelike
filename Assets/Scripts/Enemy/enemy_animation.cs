using UnityEngine;
using Pathfinding;

public class EnemyAnimation : MonoBehaviour
{
    private AIPath aiPath;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        aiPath = GetComponentInParent<AIPath>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Idle / Walk animation
        animator.SetFloat("speed", aiPath.velocity.magnitude);

        // Face the direction of movement
        if (aiPath.desiredVelocity.x > 0.05f)
        {
            spriteRenderer.flipX = false;
        }
        else if (aiPath.desiredVelocity.x < -0.05f)
        {
            spriteRenderer.flipX = true;
        }
    }
}