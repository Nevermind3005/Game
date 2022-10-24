using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float speed = 15f;
    [SerializeField] private float destroyDistance = 10f;
    private Transform firePointPosition;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        firePointPosition = GameObject.Find("FirePoint").GetComponent<Transform>();
    }

    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, firePointPosition.position) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
