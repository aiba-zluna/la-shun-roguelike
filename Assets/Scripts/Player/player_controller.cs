using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 input;
    private PlayerStats stats;
    
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1;
        if (Keyboard.current.sKey.isPressed) input.y -= 1;
        if (Keyboard.current.aKey.isPressed) input.x -= 1;
        if (Keyboard.current.dKey.isPressed) input.x += 1;

        animator.SetBool("isRunning", input != Vector2.zero);

        if (input != Vector2.zero)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteRenderer.flipX = mousePos.x < transform.position.x;
        }
    }


    void FixedUpdate()
    {
        rb.linearVelocity = input.normalized * stats.moveSpeed;
    }


}