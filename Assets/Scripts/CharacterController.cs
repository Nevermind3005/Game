using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 15;
    [Range(0, 0.3f)] [SerializeField] private float movementSmoothing = 0.05f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private float jumpFallGravity = 3.5f;
    [SerializeField] private Transform groundCheckPos;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private float horizontalInput;
    private float groundCheckRadius = 0.2f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = false;
        horizontalInput = Input.GetAxisRaw("Horizontal");

        //Check if player is on the ground
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 10f;
        }
        else
        {
            moveSpeed = 5f;
        }

        rb.velocity = new Vector2( horizontalInput * moveSpeed, rb.velocity.y);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (!isGrounded)
        {
            rb.gravityScale = jumpFallGravity;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Draw ground check
        Gizmos.DrawSphere(groundCheckPos.position, groundCheckRadius);
    }
}
