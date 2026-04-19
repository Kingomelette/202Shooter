using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;
    public GameObject deathScreen;
    public Slider healthBar;

    void Start()
    {
        // Set starting health and initialise health bar
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Show death screen and freeze the game
        if (deathScreen != null)
        {
            deathScreen.SetActive(true);
        }
        Time.timeScale = 0f;
    }
}