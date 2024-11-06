using System.Collections;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject creditsPanel;     // Panel donde se mostrar�n los cr�ditos
    public AudioSource creditsMusic;    // AudioSource para la m�sica de cr�ditos
    public string triggerTag = "Caba�ana";  // Tag de la zona que activa los cr�ditos
    public LayerMask triggerLayer;      // Layer que activa los cr�ditos

    private void Start()
    {
        // Aseg�rate de que el panel de cr�ditos est� desactivado al inicio
        creditsPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger tiene el tag o layer correcto
        if (other.CompareTag(triggerTag) || ((1 << other.gameObject.layer) & triggerLayer) != 0)
        {
            // Activa los cr�ditos cuando Morty llega al �rea
            StartCoroutine(ShowCredits());
        }
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
