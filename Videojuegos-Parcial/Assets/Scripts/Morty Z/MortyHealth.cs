using UnityEngine;
using System;

public class MortyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public event Action OnHealthChanged; // Evento para notificar cambios en la vida

    void Start()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(); // Llama al evento al iniciar
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        OnHealthChanged?.Invoke(); // Notifica el cambio de vida
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke(); // Notifica el cambio de vida
    }

    private void Die()
    {
        Debug.Log("Morty ha muerto.");
    }
}
