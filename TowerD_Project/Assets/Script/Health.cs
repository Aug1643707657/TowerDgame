using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth2D : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthFill; // drag the Fill image here in the Inspector

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        Debug.Log($"[EnemyHealth2D] Took {amount} damage. Current health: {currentHealth}");
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
        }
        else
        {
            Debug.LogWarning("[EnemyHealth2D] healthFill is not assigned.");
        }
    }

    void Die()
    {
        Debug.Log("[EnemyHealth2D] Enemy died.");
        Destroy(gameObject);
    }
}


