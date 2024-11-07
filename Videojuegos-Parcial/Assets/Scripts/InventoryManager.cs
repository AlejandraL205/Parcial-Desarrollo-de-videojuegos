using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public Text inventoryText;
    public Text levelSummaryText; // Texto UI para el resumen al final del nivel

    private Dictionary<string, int> items = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Permite que el inventario persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Actualiza la UI al iniciar el nivel para que siempre esté sincronizada
        //UpdateInventoryUI();
    }

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
            items[itemName]++;
        else
            items[itemName] = 1;

        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        //inventoryText.text = "Cerebros: " + GetItemCount("Brain") + "\nPistas: " + GetItemCount("Hint") + "\nLlaves: " + GetItemCount("Key");
    }

    public int GetItemCount(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    // Método para calcular el puntaje total al final del nivel
    public int CalculateScore()
    {
        int brains = GetItemCount("Brain") * 10; // Cada cerebro suma 10 puntos
        int hints = GetItemCount("Hint") * 5;    // Cada pista suma 5 puntos
        return brains + hints;
    }

    // Método para mostrar el resumen al final del nivel
    public void ShowLevelSummary()
    {
        int brains = GetItemCount("Brain");
        int hints = GetItemCount("Hint");
        int score = CalculateScore();

        levelSummaryText.text = $"Resumen de Nivel:\nCerebros: {brains}\nPistas: {hints}\nPuntaje Total: {score}";
        levelSummaryText.gameObject.SetActive(true); // Muestra el resumen
    }

    public void ResetInventory()
    {
        items.Clear();
        UpdateInventoryUI();
    }
}
