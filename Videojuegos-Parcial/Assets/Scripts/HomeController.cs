using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class HomeController : MonoBehaviour
{
    // Asigna estos botones desde el Inspector de Unity
    public Button playButton; // Bot�n de jugar
    public Button quitButton; // Bot�n de salir

    void Start()
    {
        // A�adir listeners a los botones
        playButton.onClick.AddListener(OnPlayButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    // M�todo que se llama cuando se hace clic en el bot�n Play
    private void OnPlayButtonClicked()
    {
        // Verifica si el primer nivel ha sido completado
        if (PlayerPrefs.GetInt("Nivel1Completo", 0) == 1)
        {
            // Si el nivel 1 est� completo, carga el Nivel 2
            SceneManager.LoadScene("Nivel 2");
        }
        else
        {
            // Si el nivel 1 no est� completo, carga el Nivel 1
            SceneManager.LoadScene("Nivel 1");
        }
    }

    // M�todo que se llama cuando se hace clic en el bot�n Quit
    private void OnQuitButtonClicked()
    {
        // Cierra la aplicaci�n
        Application.Quit();
        // Si estamos en el editor de Unity, esto no cerrar� la aplicaci�n, as� que a�adimos la siguiente l�nea
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el modo de juego en el editor
#endif
    }
}
