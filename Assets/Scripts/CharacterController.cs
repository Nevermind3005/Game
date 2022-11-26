using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float acceleration = 7f;
    [SerializeField] private float deceleration = 7f;
    [SerializeField] private float velPower = 0.9f;
    [SerializeField] private float friction = 0.2f;
    [SerializeField] private ParticleSystem dustParticle;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float jumpFallGravity = 1.5f;

    [Header("Dashing")]
    [SerializeField] private float dashVelocity = 10f;
    [SerializeField] private float dashTime = 0.5f;
    //[SerializeField] private TrailRenderer trailRenderer;

    [Header("Ground Check")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;


    private Rigidbody2D rb;
    private float normalGravity;
    private bool facingRight = true;

    //Input
    private PlayerInputActions playerInputActions;
    private float horizontalInput;

    //Dash
    private Vector2 dashDirection;
    [HideInInspector] public bool isDashing;
    private bool canDash = true;

    //Ground check
    private bool isGrounded = true;

    private bool canJump = true;

    private Animator animator;

    //Init
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
        normalGravity = rb.gravityScale;
    }
    
    void FixedUpdate()
    {
        isGrounded = false;
        
        //Get input from the player
        horizontalInput = playerInputActions.Player.Move.ReadValue<float>();

        float targetSpeed = horizontalInput * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        //Check if player is on the ground
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheckPos.position, new Vector2(0.5f, 0.05f), 0); ;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject && (groundLayer & (1 << colliders[i].gameObject.layer)) != 0)
            {
                isGrounded = true;
            }
        }

        //Flip player
        if (horizontalInput > 0 && !facingRight)
        {
            flip();
            if (Mathf.Abs(rb.velocity.x) > 5.5f) 
            {
                ShowDustParticle();
            }
        }
        
        //Flip player
        if (horizontalInput < 0 && facingRight)
        {
            flip();
            if (Mathf.Abs(rb.velocity.x) > 5.5f) 
            {
                ShowDustParticle();
            }
        }

        //Dash
        if (playerInputActions.Player.Dash.IsPressed() && canDash)
        {
            isDashing = true;
            canDash = false;
            switch (facingRight)
            {
                case true:
                    dashDirection = new Vector2(1f, 0f);
                    break;
                case false:
                    dashDirection = new Vector2(-1f, 0f);
                    break;
            }
           
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.AddForce(dashDirection.normalized * dashVelocity, ForceMode2D.Impulse);
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            return;
        }

        if (isGrounded)
        {
            if (!playerInputActions.Player.Dash.IsPressed())
            {
                canDash = true;
            }
        }

        //Move player
        rb.AddForce(movement * Vector2.right);

        SetWalkAnimation();

        if (isGrounded && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        //Jump
        if (playerInputActions.Player.Jump.IsPressed() && isGrounded && canJump)
        {
            canJump = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("jumping", true);
        }

        if (!playerInputActions.Player.Jump.IsPressed() && isGrounded)
        {
            canJump = true;
            animator.SetBool("jumping", false);
        }
        
        //Set higher scale of gravity if player is falling
        if (rb.velocity.y < 0) {
            rb.gravityScale = jumpFallGravity;
        } 
        else
        {
            rb.gravityScale = normalGravity;
        }
    }

    private void SetWalkAnimation()
    {
        if ((horizontalInput > 0.0f || horizontalInput < 0.0f) && isGrounded)
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }
    }

    private void flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
    
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }

    private IEnumerator EnablePlayerControls(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        playerInputActions.Player.Enable();
        playerInputActions.Weapon.Enable();
    }

    private void ShowDustParticle()
    {
        dustParticle.Play();
    }
    
    //Debug gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw ground check
        Gizmos.DrawCube(groundCheckPos.position, new Vector2(0.5f, 0.05f));
    }

    public void DisablePlayerControls(float waitTime)
    {
        playerInputActions.Player.Disable();
        playerInputActions.Weapon.Disable();
        StartCoroutine(EnablePlayerControls(waitTime));
    }

    public void DisableCollider()
    {
        
    }
}
