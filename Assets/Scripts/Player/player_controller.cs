using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 movement;
    private PlayerStats stats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        movement = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            movement.y = 1;

        if (Keyboard.current.sKey.isPressed)
            movement.y = -1;

        if (Keyboard.current.aKey.isPressed)
            movement.x = -1;

        if (Keyboard.current.dKey.isPressed)
            movement.x = 1;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * stats.moveSpeed;
    }
}