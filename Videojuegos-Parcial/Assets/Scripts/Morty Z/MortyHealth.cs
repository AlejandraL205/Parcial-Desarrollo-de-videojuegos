using UnityEngine;

public class MortyHealth : MonoBehaviour
{
    // Barra de vida (cerebros)
    public int maxHealth = 3; // M�ximo de cerebros
    private int currentHealth; // Vida actual

    void Start()
    {
        // Inicializa la vida actual en el m�ximo
        currentHealth = maxHealth;
    }

    // Funci�n para reducir la vida de Morty al recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Recuperaci�n de vida al recoger un cerebro
    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    // Funci�n que se llama al perder toda la vida
    private void Die()
    {
        Debug.Log("Morty ha muerto.");
        // L�gica adicional al morir (resetear nivel, pantalla de Game Over, etc.)
    }

    // Para visualizar la barra de vida en el inspector
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Vida: " + currentHealth + "/" + maxHealth);
    }
}
