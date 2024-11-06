using UnityEngine;

public class MortyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private Vector2 movement; // Vector para almacenar la dirección de movimiento

    [Header("Gas Particles")]
    public ParticleSystem gasParticles; // Referencia al sistema de partículas del gas
    private bool gasActive = false; // Indica si el gas está activado

    [Header("Stats")]
    public MortyHealth health; // Referencia al sistema de salud de Morty
    public CerebrosUI cerebroUI; // UI para mostrar los cerebros recolectados

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ActivateGas();
        }

        //if (Input.GetKeyDown(KeyCode.E)) CollectBrainOrKey();
        //if (Input.GetKeyDown(KeyCode.R)) CollectClue();
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
        gasActive = !gasActive;
        if (gasActive)
        {
            gasParticles.Play();
            Debug.Log("Gas activado: aturdiendo a los cazadores.");
        }
        else
        {
            gasParticles.Stop();
            Debug.Log("Gas desactivado.");
        }
    }
}
