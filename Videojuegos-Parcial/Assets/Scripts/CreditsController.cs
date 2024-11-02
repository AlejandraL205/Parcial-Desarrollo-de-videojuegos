using System.Collections;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject creditsPanel;     // Panel donde se mostrarán los créditos
    public AudioSource creditsMusic;    // AudioSource para la música de créditos

    private void Start()
    {
        // Asegúrate de que el panel de créditos esté desactivado al inicio
        creditsPanel.SetActive(false);

        // Inicia la corrutina para mostrar los créditos
        StartCoroutine(ShowCredits());
    }

    private IEnumerator ShowCredits()
    {
        // Activa el panel de créditos y la música
        creditsPanel.SetActive(true);
        creditsMusic.Play();

        // Espera a que la música suene (puedes ajustar el tiempo según sea necesario)
        yield return new WaitForSeconds(creditsMusic.clip.length);

        // Desactiva el panel de créditos después de que la música termine
        creditsPanel.SetActive(false);
    }
}
