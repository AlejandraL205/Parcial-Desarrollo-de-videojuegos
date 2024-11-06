using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MortyHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;
    public event Action OnHealthChanged; // Evento para notificar cambios en la vida
    public int brainsCollected; // Cerebros recolectados (puntos extra)

    void Start()
    {
        currentHealth = maxHealth;
        brainsCollected = 0;
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
        OnHealthChanged?.Invoke();
    }

    public void HealOrIncreaseScore()
    {
        if (currentHealth < maxHealth)
        {
            Heal(1); // Aumenta la salud si no tiene la vida máxima
        }
        else
        {
            brainsCollected++; // Sumar al puntaje de cerebros si ya tiene la vida máxima
        }
        OnHealthChanged?.Invoke();
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        OnHealthChanged?.Invoke();
    }

    private void Die()
    {
        Debug.Log("Morty ha muerto.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena en caso de muerte
    }
}
