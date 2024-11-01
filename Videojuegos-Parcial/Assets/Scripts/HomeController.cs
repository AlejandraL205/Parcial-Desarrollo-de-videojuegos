using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class HomeController : MonoBehaviour
{
    // Asigna estos botones desde el Inspector de Unity
    public Button playButton; // Botón de jugar
    public Button quitButton; // Botón de salir

    void Start()
    {
        // Añadir listeners a los botones
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // Método que se llama cuando se hace clic en el botón Play
    private void OnPlayButtonClicked()
    {
        // Verifica si el primer nivel ha sido completado
        if (PlayerPrefs.GetInt("Nivel1Completo", 0) == 1)
        {
            // Si el nivel 1 está completo, carga el Nivel 2
            SceneManager.LoadScene("Nivel 2");
        }
        else
        {
            // Si el nivel 1 no está completo, carga el Nivel 1
            SceneManager.LoadScene("Nivel 1");
        }
    }

    // Método que se llama cuando se hace clic en el botón Quit
    private void OnQuitButtonClicked()
    {
        // Cierra la aplicación
        Application.Quit();
        // Si estamos en el editor de Unity, esto no cerrará la aplicación, así que añadimos la siguiente línea
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el modo de juego en el editor
#endif
    }
}
