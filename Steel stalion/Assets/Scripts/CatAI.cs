using UnityEngine;
using UnityEngine.AI;

public class CatAI : MonoBehaviour
{
    public float detectionRange = 20f;
    public float fieldOfViewAngle = 90f;
    public float fireRate = 1.5f;
    public float projectileSpeed = 15f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Transform player;
    public float health = 60f;

    private NavMeshAgent agent;
    private float nextFireTime = 0f;
    private Vector3 lastKnownPosition;
    private bool hasLineOfSight = false;

    enum State { Idle, Shooting, Chasing }
    State currentState = State.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        hasLineOfSight = CheckLineOfSight();

        switch (currentState)
        {
            case State.Idle:
                if (hasLineOfSight)
                    currentState = State.Shooting;
                break;

            case State.Shooting:
                agent.isStopped = true;
                transform.LookAt(new Vector3(
                    player.position.x,
                    transform.position.y,
                    player.position.z
                ));

                if (hasLineOfSight)
                {
                    lastKnownPosition = player.position;
                    if (Time.time >= nextFireTime)
                    {
                        nextFireTime = Time.time + fireRate;
                        FireProjectile();
                    }
                }
                else
                {
                    currentState = State.Chasing;
                }
                break;

            case State.Chasing:
                agent.isStopped = false;
                agent.SetDestination(lastKnownPosition);

                if (hasLineOfSight)
                    currentState = State.Shooting;

                if (Vector3.Distance(transform.position, lastKnownPosition) < 1f)
                    currentState = State.Idle;
                break;
        }
    }

    bool CheckLineOfSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > detectionRange)
            return false;

        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        if (angle > fieldOfViewAngle / 2f)
            return false;

        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer.normalized, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Player"))
                return true;
        }

        return false;
    }

    void FireProjectile()
    {
        if (projectilePrefab == null || firePoint == null) return;

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        Vector3 direction = (player.position - firePoint.position).normalized;
        rb.linearVelocity = direction * projectileSpeed;

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