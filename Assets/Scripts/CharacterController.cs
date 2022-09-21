using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private float jumpFallGravity = 3.5f;
    [SerializeField] private Transform groundCheckPoint;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private float horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        isGrounded = false;
        RaycastHit2D groundHit =
            Physics2D.BoxCast(groundCheckPoint.position, new Vector2(1f, 0.01f), 0, Vector2.down, 0.01f);
        horizontalInput = Input.GetAxisRaw("Horizontal");
        if (groundHit.collider is not null)
        {
            if (groundHit.collider.tag == "Ground")
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheckPoint.position, new Vector2(1f, 0.01f));
    }
}
