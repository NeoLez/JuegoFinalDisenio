using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ExplodingEnemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private LayerMask playerLayer;
    
    [Header("Chase")]
    [SerializeField] private float chaseSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    
    [Header("Explosion")]
    [SerializeField] private float explosionRange = 3f;
    [SerializeField] private GameObject explosionEffectPrefab; 
    [SerializeField] private AudioClip explosionSound; 
    
    [Header("Visual Feedback")]
    [SerializeField] private Renderer enemyRenderer;
    [SerializeField] private Color idleColor = Color.blue;
    [SerializeField] private Color chasingColor = Color.red;
    [SerializeField] private float blinkSpeed = 5f; // Velocidad de parpadeo cuando está cerca
    
    private Transform player;
    private Rigidbody rb;
    private bool isChasing = false;
    private float distanceToPlayer;
    
    private void Start()
    {
        player = GameManager.Player.transform;
        rb = GetComponent<Rigidbody>();
        
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = idleColor;
        }
    }
    
    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (!isChasing && distanceToPlayer <= detectionRange)
        {
            StartChasing();
        }
        
        if (isChasing && distanceToPlayer <= explosionRange)
        {
            Explode();
        }
        
        if (isChasing && distanceToPlayer <= explosionRange * 1.5f && enemyRenderer != null)
        {
            float lerp = Mathf.PingPong(Time.time * blinkSpeed, 1);
            enemyRenderer.material.color = Color.Lerp(chasingColor, Color.white, lerp);
        }
    }
    
    private void FixedUpdate()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
    }
    
    private void StartChasing()
    {
        isChasing = true;
        Debug.Log("prepara el ojete");
        
        if (enemyRenderer != null)
        {
            enemyRenderer.material.color = chasingColor;
        }
    }
    
    private void ChasePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        
        Vector3 moveVelocity = direction * chaseSpeed;
        moveVelocity.y = rb.velocity.y; // Mantener velocidad vertical 
        rb.velocity = moveVelocity;
        
        // Rotar hacia el jugador
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
    
    private void Explode()
    {
        Debug.Log("kaboom!");
        
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.Die();
        }
        
        if (explosionEffectPrefab != null)
        {
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
        }
        
        if (explosionSound != null && GameManager.AudioSystem != null)
        {
            GameManager.AudioSystem.PlaySound(explosionSound);
        }
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}