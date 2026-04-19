using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Projectile triggered: " + other.name + " tag: " + other.tag);
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerHealth>().TakeDamage(damage);
            Debug.Log("Player hit!");
        }
        Destroy(gameObject);
    }
}
