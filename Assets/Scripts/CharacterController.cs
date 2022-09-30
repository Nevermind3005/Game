using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7;
    [SerializeField] private float acceleration = 7f;
    [SerializeField] private float decceleration = 7f;
    [SerializeField] private float velPower = 0.9f;
    [SerializeField] private float friction = 0.2f;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 7.5f;
    [SerializeField] private float jumpFallGravity = 1.5f;

    [Header("Dashing")]
    [SerializeField] private float dashVelocity = 10f;
    [SerializeField] private float dashTime = 0.5f;
    [SerializeField] private TrailRenderer trailRenderer;

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
    private bool isDashing;
    private bool canDash = true;

    //Ground check
    private float groundCheckRadius = 0.01f;
    private bool isGrounded = true;

    //Init
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        //Check if player is on the ground
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundCheckPos.position, new Vector2(0.5f, 0.01f), 0); ;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        if (horizontalInput > 0 && !facingRight)
        {
            flip();
        }

        if (horizontalInput < 0 && facingRight)
        {
            flip();
        }

        if (playerInputActions.Player.Dash.IsPressed() && canDash)
        {
            isDashing = true;
            canDash = false;
            dashDirection = new Vector2(horizontalInput, 0f);
            if (dashDirection.x == 0)
            {
                dashDirection = new Vector2(transform.localScale.x, 0f);
            }
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            rb.AddForce(dashDirection.normalized * dashVelocity, ForceMode2D.Impulse);
            trailRenderer.emitting = true;
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            //rb.velocity = dashDirection.normalized * dashVelocity;
            return;
        }

        if (isGrounded)
        {
            canDash = true;
        }

        //Move player
        rb.AddForce(movement * Vector2.right);

        if (isGrounded && Mathf.Abs(horizontalInput) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(friction));

            amount *= Mathf.Sign(rb.velocity.x);

            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }

        //Jump
        if (playerInputActions.Player.Jump.IsPressed() && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //Set higher scale of gravity if player is falling
        if (rb.velocity.y < 0) {
            rb.gravityScale = jumpFallGravity;
        } else
        {
            rb.gravityScale = normalGravity;
        }
    }

    private void flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        trailRenderer.emitting = false;
    }

    
    //Debug gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw ground check
        Gizmos.DrawCube(groundCheckPos.position, new Vector2(0.5f, 0.01f));
    }
}
