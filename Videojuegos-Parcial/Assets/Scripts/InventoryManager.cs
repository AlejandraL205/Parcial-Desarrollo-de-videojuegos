using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Text inventoryText; // Texto de UI para mostrar el inventario en pantalla

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items[itemName] = 1;

        UpdateInventoryUI(); // Actualiza el UI después de agregar un ítem
    }

    private void UpdateInventoryUI()
    {
        // Actualiza el texto del inventario con la cantidad de cerebros, pistas y llaves
        inventoryText.text = "Cerebros: " + GetItemCount("Brain") + "\nPistas: " + GetItemCount("Hint") + "\nLlaves: " + GetItemCount("Key");
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }
}
