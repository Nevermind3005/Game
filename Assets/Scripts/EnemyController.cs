using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float direction = 1;
    [SerializeField] private Transform wallCheckPoint;

    private Rigidbody2D rb;
    private Animator animator;
    [HideInInspector] public Health health = new (3);
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health.CurrentHealth == 0)
        {
            animator.SetBool("death", true);
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        var hit = Physics2D.Raycast(wallCheckPoint.position, new Vector2(direction, 0f), 0.1f);
        Debug.DrawRay(wallCheckPoint.position, new Vector2(direction, 0f), Color.red);

            if (hit.collider is not null)
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Platform")
                {
                    transform.Rotate(0f, 180f, 0f);
                    direction *= -1;
                }
            }

        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            Debug.Log("Hit by player");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviour>().DamagePlayer();
            var dir = col.gameObject.GetComponent<Transform>().rotation.y < 0 ? 1 : -1;
            col.gameObject.GetComponent<CharacterController>().DisablePlayerControls(0.3f);
            col.gameObject.GetComponent<PlayerBehaviour>().DisableEnemyCollision();
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2( dir * 22, 7), ForceMode2D.Impulse);
        }
    }
}
