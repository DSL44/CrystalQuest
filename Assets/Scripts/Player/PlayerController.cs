using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float jumpForce = 13f;

    [Header("Ground")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    [Header("Collider Agachado")]
    public Vector2 crouchSize = new Vector2(0.55f, 0.9f);
    public Vector2 crouchOffset = new Vector2(0f, -0.30f);

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsule;
    private PlayerInputActions inputActions;

    private Vector2 moveInput;

    private bool crouching;

    private Vector2 normalSize;
    private Vector2 normalOffset;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();

        normalSize = capsule.size;
        normalOffset = capsule.offset;

        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMoveCanceled;

        inputActions.Player.Jump.performed += OnJump;

        inputActions.Player.Crouch.performed += OnCrouch;
        inputActions.Player.Crouch.canceled += OnCrouchCanceled;

        inputActions.Player.Quit.performed += OnQuit;
    }

    void OnDisable()
{
    if (inputActions == null)
        return;

    inputActions.Player.Move.performed -= OnMove;
    inputActions.Player.Move.canceled -= OnMoveCanceled;

    inputActions.Player.Jump.performed -= OnJump;

    inputActions.Player.Crouch.performed -= OnCrouch;
    inputActions.Player.Crouch.canceled -= OnCrouchCanceled;

    inputActions.Player.Quit.performed -= OnQuit;

    

    inputActions.Disable();
}

    void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        if (crouching)
            return;

        if (!IsGrounded())
            return;

        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            jumpForce);
    }

    void OnCrouch(InputAction.CallbackContext ctx)
    {
        crouching = true;

        capsule.size = crouchSize;
        capsule.offset = crouchOffset;
    }

    void OnCrouchCanceled(InputAction.CallbackContext ctx)
    {
        crouching = false;

        capsule.size = normalSize;
        capsule.offset = normalOffset;
    }

    void Update()
    {
        bool grounded = IsGrounded();

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Crouch", crouching);

        float vertical = rb.linearVelocity.y;

        if (Mathf.Abs(vertical) < 0.1f)
            vertical = 0;

        animator.SetFloat("VerticalSpeed", vertical);

        if (moveInput.x > 0.1f)
            spriteRenderer.flipX = false;
        else if (moveInput.x < -0.1f)
            spriteRenderer.flipX = true;
    }

    void FixedUpdate()
    {
        float currentSpeed = crouching ? crouchSpeed : speed;

        rb.linearVelocity = new Vector2(
            moveInput.x * currentSpeed,
            rb.linearVelocity.y);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            0.3f,
            groundLayer);

        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawLine(
            groundCheck.position,
            groundCheck.position + Vector3.down * 0.3f);
    }

    void OnQuit(InputAction.CallbackContext ctx)
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}