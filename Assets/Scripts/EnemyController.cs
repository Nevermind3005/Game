using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float direction = 1;
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private Light2D headLight;
    [SerializeField] private AudioClip clip;

    private Rigidbody2D rb;
    private Animator animator;
    private AudioSource audioSource;
    [HideInInspector] public Health health = new (3);
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health.CurrentHealth == 0)
        {
            animator.SetBool("death", true);
            headLight.intensity = 0;
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
                if (hit.collider.tag == "Platform" || hit.collider.tag == "Enemy" || hit.collider.tag == "DroidNav")
                {
                    transform.Rotate(0f, 180f, 0f);
                    direction *= -1;
                }
            }

        rb.velocity = new Vector2(moveSpeed * direction, rb.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerBehaviour>().DamagePlayer();
        }
    }
}
