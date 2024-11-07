using UnityEngine;

public class Cerebro : MonoBehaviour
{
    public int healthRestoreValue = 1; // Valor de vida que recupera el cerebro

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Morty"))
        {
            // Recuperar salud o aumentar puntuación
            if (collision.GetComponent<MortyHealth>().currentHealth < collision.GetComponent<MortyHealth>().maxHealth)
            {
                collision.GetComponent<MortyHealth>().Heal(healthRestoreValue); // Recupera vida
                Debug.Log("Recuperando vida de cerebro.");
            }
            else
            {
                // Aumentar puntuación por cerebro comido
                Debug.Log("Cerebro comido, aumento el puntaje.");
            }

            Destroy(gameObject); // Destruir el objeto de cerebro
        }
    }
}
