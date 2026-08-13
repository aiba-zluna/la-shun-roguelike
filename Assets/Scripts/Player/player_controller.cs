using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CooldownUI cooldownUI;
    private InputSystem_Actions input;

    private Rigidbody2D rb;
    private Vector2 movementInput;
    private PlayerStats stats;

    public float dashForce;
    public float dashDuration;
    private bool canDash = true;
    private bool isDashing;
    private bool startDashingCooldown;

    private Vector3 mousePos;
    private Vector2 dashDirection;
    [SerializeField] public Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool directionLocked;

    void Awake()
    {
        input = new InputSystem_Actions();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<PlayerStats>();
    }

    void OnEnable()
    {
        input.Player.Enable();
        input.Player.Move.performed += movement;
        input.Player.Dash.performed += dash;

        input.Player.Dash.canceled += dash;
        input.Player.Move.canceled += movement;
    }

    void OnDisable()
    {
        input.Player.Disable();
        input.Player.Move.performed -= movement;
        input.Player.Move.canceled -= movement;
        input.Player.Dash.performed -= dash;
        input.Player.Dash.canceled -= dash;
    }

    void movement(InputAction.CallbackContext context)
    {
        
        movementInput = context.ReadValue<Vector2>();

        if (context.performed)
        {
            animator.SetBool("isRunning",true);
        }
        else
        {
            animator.SetBool("isRunning",false);
        }
    }

    void dash(InputAction.CallbackContext context)
    {
        if (!canDash || isDashing) return;

        if (context.performed)
        {
            StartCoroutine(DashCoroutine());
        }
    }
    IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        Vector2 dashDirection = movementInput.normalized;

        if(dashDirection == Vector2.zero)
        {
            isDashing = false;
            canDash = true;
            yield break;
        }
        rb.linearVelocity = dashDirection * dashForce;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        startDashingCooldown = true;
        cooldownUI.StartCooldown(stats.dash);
        yield return new WaitForSeconds(stats.dash);
        canDash = true;
        startDashingCooldown = false;
    }

    public bool DashCheck()
    {
        return isDashing;
    }
    

    void MouseRotation()
    {
        if (directionLocked)
        return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x < transform.position.x && movementInput != Vector2.zero)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        MouseRotation();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.linearVelocity = movementInput.normalized * stats.moveSpeed;
        }
    }

    public void SetDirectionLocked(bool locked)
    {
        directionLocked = locked;
    }


}