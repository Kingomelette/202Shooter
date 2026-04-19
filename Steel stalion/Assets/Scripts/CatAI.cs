using UnityEngine;
using UnityEngine.AI;

public class CatAI : MonoBehaviour
{
    public float fireRate = 1.5f;
    public float projectileSpeed = 15f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float health = 60f;

    private NavMeshAgent agent;
    private Transform player;
    private float nextFireTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // Find the player by tag
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        // Always chase and face the player
        agent.SetDestination(player.position);
        transform.LookAt(new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z
        ));

        // Fire at set intervals
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            FireProjectile();
        }
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * projectileSpeed;
        // Prevent projectile hitting the enemy that fired it
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), GetComponent<Collider>());
        Destroy(projectile, 5f);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}