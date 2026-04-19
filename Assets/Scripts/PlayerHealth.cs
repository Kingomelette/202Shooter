using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public UnityEngine.UI.Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        Debug.Log("Player died");
    }
}
