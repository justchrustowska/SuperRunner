using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    public int currentHealth { get; private set; }
    
    public delegate void HealthChanged(int currentHealth);
    public event HealthChanged OnHealthChanged;

    public delegate void PlayerDied();
    public event PlayerDied OnPlayerDied;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
        }
    }
}
