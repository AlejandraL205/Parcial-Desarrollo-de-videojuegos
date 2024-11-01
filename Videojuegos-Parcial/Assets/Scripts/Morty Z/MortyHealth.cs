using UnityEngine;

public class MortyHealth : MonoBehaviour
{
    // Barra de vida (cerebros)
    public int maxHealth = 3; // Máximo de cerebros
    private int currentHealth; // Vida actual

    void Start()
    {
        // Inicializa la vida actual en el máximo
        currentHealth = maxHealth;
    }

    // Función para reducir la vida de Morty al recibir daño
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Recuperación de vida al recoger un cerebro
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Función que se llama al perder toda la vida
    private void Die()
    {
        Debug.Log("Morty ha muerto.");
        // Lógica adicional al morir (resetear nivel, pantalla de Game Over, etc.)
    }

    // Para visualizar la barra de vida en el inspector
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Vida: " + currentHealth + "/" + maxHealth);
    }
}
