using UnityEngine;
using UnityEngine.SceneManagement; // Aseg�rate de importar SceneManagement

public class LevelEndManager : MonoBehaviour
{
    public string nextLevelName; // Nombre de la siguiente escena (nivel) a cargar

    public void OnLevelEnd()
    {
        // Muestra el resumen del nivel
        InventoryManager.instance.ShowLevelSummary();
    }

    public void NextLevel()
    {
        // Resetea el inventario antes de cargar el pr�ximo nivel
        InventoryManager.instance.ResetInventory();

        // Verifica si se ha configurado el nombre de la siguiente escena
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            // Carga la escena del siguiente nivel
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("El nombre del pr�ximo nivel no est� configurado en el LevelEndManager.");
        }
    }
}
