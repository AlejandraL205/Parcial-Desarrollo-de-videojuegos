using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI
using System.Collections; // Necesario para usar Coroutines

public class MortyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private Vector2 movement; // Vector para almacenar la dirección de movimiento

    [Header("Gas Particles")]
    public ParticleSystem gasParticles; // Referencia al sistema de partículas del gas
    private bool gasActive = false; // Indica si el gas está activado

    [Header("Gas HUD")]
    public Slider gasSlider; // Slider que muestra el poder del gas
    public float gasCooldown = 3f; // Tiempo de recarga para el gas en segundos

    [Header("Stats")]
    public MortyHealth health; // Referencia al sistema de salud de Morty
    public CerebrosUI cerebroUI; // UI para mostrar los cerebros recolectados

    private bool canActivateGas = true; // Si el gas puede ser activado (deshabilitado durante el cooldown)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (gasParticles != null)
        {
            gasParticles.Stop();
        }
        else
        {
            Debug.LogError("No se ha asignado un sistema de partículas de gas en el Inspector.");
        }

        // Inicializamos la barra de gas al máximo (completamente llena).
        if (gasSlider != null)
        {
            gasSlider.value = 1; // Slider lleno
        }
    }

    void Update()
    {
        movement.x = 0;
        movement.y = 0;

        if (Input.GetKey(KeyCode.W)) movement.y = 1;
        if (Input.GetKey(KeyCode.S)) movement.y = -1;
        if (Input.GetKey(KeyCode.A)) movement.x = -1;
        if (Input.GetKey(KeyCode.D)) movement.x = 1;

        movement.Normalize();
        RotateCharacter();

        // Activar gas con la barra espaciadora si está disponible
        if (Input.GetKeyDown(KeyCode.Space) && canActivateGas)
        {
            ActivateGas();
        }

        // Si el gas está activado, vaciar el slider
        if (gasActive && gasSlider != null)
        {
            gasSlider.value = 0; // Vaciar el slider cuando el gas está activado
        }
        else if (!gasActive && gasSlider != null)
        {
            gasSlider.value = 1; // Llenar el slider cuando el gas no está activado
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void RotateCharacter()
    {
        float angle = 0;
        if (movement.y > 0) angle = 0;
        else if (movement.y < 0) angle = 180;
        else if (movement.x < 0) angle = 270;
        else if (movement.x > 0) angle = 90;

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    private void ActivateGas()
    {
        gasActive = true;
        canActivateGas = false; // Deshabilitar la posibilidad de activar el gas

        if (gasParticles != null)
        {
            gasParticles.Play();
            Debug.Log("Gas activado: aturdiendo a los cazadores.");
        }

        // Llamamos al método para aturdir al cazador cuando se activa el gas
        HunterController[] hunters = FindObjectsOfType<HunterController>();
        foreach (HunterController hunter in hunters)
        {
            hunter.StunHunter(); // Aturdir a todos los cazadores
        }

        // Reproducir el sonido cuando se activa el gas
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play(); // Reproducir el sonido del gas
        }

        // Iniciar la recarga del gas (Cooldown de 3 segundos)
        StartCoroutine(GasCooldown());
    }


    // Coroutine para manejar el tiempo de recarga del gas
    private IEnumerator GasCooldown()
    {
        yield return new WaitForSeconds(gasCooldown); // Esperar 3 segundos
        gasActive = false; // Desactivar el gas
        Debug.Log("Gas desactivado.");

        // Esperar a que pase el cooldown antes de permitir activar nuevamente el gas
        canActivateGas = true;

        // Detener las partículas del gas
        if (gasParticles != null)
        {
            gasParticles.Stop();
        }
    }
}
