using UnityEngine;

public class Weapon : MonoBehaviour
{

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shootingDelay = 1f;

    [Header("Audio")] 
    [SerializeField] private AudioClip shootClip;

    private AudioSource audioSource;
    
    private float lastShotTime = 0f;

    private PlayerInputActions playerInputActions;
    private CharacterController characterController;

    void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Weapon.Enable();
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputActions.Weapon.Shoot.IsPressed() && lastShotTime + shootingDelay < Time.time && !characterController.isDashing)
        {
            lastShotTime = Time.time;
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        audioSource.PlayOneShot(shootClip);
    }
}
