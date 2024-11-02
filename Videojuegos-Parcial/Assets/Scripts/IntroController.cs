using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class IntroController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Asigna tu VideoPlayer en el Inspector
    public AudioSource audioSource;  // Asigna tu AudioSource en el Inspector
    public GameObject introPanel;    // Asigna tu panel de introducci�n en el Inspector
    public GameObject player;         // Asigna tu objeto de jugador en el Inspector

    private void Start()
    {
        // Aseg�rate de que el video player est� desactivado inicialmente
        videoPlayer.gameObject.SetActive(false);
        player.SetActive(false); // Aseg�rate de que el jugador est� desactivado al inicio

        // Inicia la pantalla negra y el audio
        audioSource.Play();

        // Inicia la corrutina para manejar la secuencia
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        // Espera a que termine el audio del grito
        yield return new WaitForSeconds(audioSource.clip.length);

        // Muestra el video
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();

        // Espera a que termine el video
        yield return new WaitForSeconds((float)videoPlayer.length);

        // Espera 3 segundos antes de continuar
        yield return new WaitForSeconds(3f);

        // Oculta el panel de introducci�n y el video
        introPanel.SetActive(false);
        videoPlayer.gameObject.SetActive(false);

        // Muestra el jugador y cualquier otro elemento del nivel
        player.SetActive(true);

        // Si hay otros elementos del nivel que deseas mostrar, act�valos aqu�
    }
}
