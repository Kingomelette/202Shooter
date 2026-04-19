using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter(Collider other)
    {
        // Deal damage if projectile hits the player, then destroy itself
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}