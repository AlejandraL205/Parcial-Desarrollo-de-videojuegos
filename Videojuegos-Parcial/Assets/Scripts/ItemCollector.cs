using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public MortyHealth mortyHealth; // Referencia al sistema de salud de Morty
    public CerebrosUI cerebroUI;    // Referencia al UI de cerebros recolectados (si lo deseas, puedes integrar este script a tu UI para reflejar la cantidad de cerebros)

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Brain") && Input.GetKeyDown(KeyCode.E))
        {
            // Si Morty tiene menos de 3 vidas, sumar un corazón
            if (mortyHealth.currentHealth < mortyHealth.maxHealth)
            {
                mortyHealth.Heal(1); // Aumenta la salud de Morty al recolectar cerebros
                InventoryManager.instance.AddItem("Brain"); // Añade el cerebro al inventario
                Debug.Log("Cerebro recolectado y salud aumentada.");
            }
            else
            {
                // Si Morty tiene las 3 vidas completas, sumar al puntaje de cerebros recolectados
                mortyHealth.brainsCollected++; // Sumar el cerebro al puntaje
                InventoryManager.instance.AddItem("Brain"); // Añade el cerebro al inventario
                Debug.Log("Cerebro recolectado y sumado al puntaje.");
            }

            Destroy(collision.gameObject); // Destruye el objeto de cerebro recolectado
        }
        else if (collision.CompareTag("Hint") && Input.GetKeyDown(KeyCode.R))
        {
            InventoryManager.instance.AddItem("Hint"); // Añade la pista al inventario
            Destroy(collision.gameObject); // Destruye el objeto de pista recolectada
        }
        else if (collision.CompareTag("Key") && Input.GetKeyDown(KeyCode.E))
        {
            InventoryManager.instance.AddItem("Key"); // Añade la llave al inventario
            Destroy(collision.gameObject); // Destruye el objeto de llave recolectada
        }
    }
}
