using System.Collections;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject creditsPanel;     // Panel donde se mostrar�n los cr�ditos
    public AudioSource creditsMusic;    // AudioSource para la m�sica de cr�ditos

    private void Start()
    {
        // Aseg�rate de que el panel de cr�ditos est� desactivado al inicio
        creditsPanel.SetActive(false);

        // Inicia la corrutina para mostrar los cr�ditos
        StartCoroutine(ShowCredits());
    }

    private IEnumerator ShowCredits()
    {
        // Activa el panel de cr�ditos y la m�sica
        creditsPanel.SetActive(true);
        creditsMusic.Play();

        // Espera a que la m�sica suene (puedes ajustar el tiempo seg�n sea necesario)
        yield return new WaitForSeconds(creditsMusic.clip.length);

        // Desactiva el panel de cr�ditos despu�s de que la m�sica termine
        creditsPanel.SetActive(false);
    }
}
