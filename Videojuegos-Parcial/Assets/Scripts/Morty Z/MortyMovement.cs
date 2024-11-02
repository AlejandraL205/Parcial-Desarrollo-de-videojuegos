using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortyMovement : MonoBehaviour
{
    // Velocidad de movimiento del personaje
    public float moveSpeed = 5f;
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private Vector2 movement; // Vector para almacenar la dirección de movimiento

    // Grados de rotación por dirección
    private float upRotation = 0f;       // Rotación hacia arriba
    private float downRotation = 180f;   // Rotación hacia abajo
    private float leftRotation = 270f;   // Rotación hacia la izquierda
    private float rightRotation = 90f;   // Rotación hacia la derecha

    [Header("Gas Particles")]
    public ParticleSystem gasParticles; // Referencia al sistema de partículas del gas

    void Start()
    {
        // Obtener la referencia al Rigidbody2D del jugador
        rb = GetComponent<Rigidbody2D>();

        // Asegúrate de que el gas no se reproduzca al iniciar el juego
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
        // Detectar el input del jugador
        movement.x = 0; // Reiniciar el movimiento horizontal
        movement.y = 0; // Reiniciar el movimiento vertical

        // Comprobar las teclas específicas para el movimiento
        if (Input.GetKey(KeyCode.W)) // Mover arriba
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S)) // Mover abajo
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A)) // Mover izquierda
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D)) // Mover derecha
        {
            movement.x = 1;
        }

        movement.Normalize(); // Normalizar el movimiento para mantener la misma velocidad en diagonal
        RotateCharacter(); // Llamar a la función para rotar el personaje

        // Activar y desactivar el gas con la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space) && gasParticles != null)
        {
            gasParticles.Play(); // Iniciar el gas
        }
        else if (Input.GetKeyUp(KeyCode.Space) && gasParticles != null)
        {
            gasParticles.Stop(); // Detener el gas
        }

        // Interacciones
        if (Input.GetKeyDown(KeyCode.E)) // Recoger Cerebro o Llave
        {
            CollectBrainOrKey();
        }
        if (Input.GetKeyDown(KeyCode.R)) // Recoger Pista
        {
            CollectClue();
        }
    }

    void FixedUpdate()
    {
        // Mover a Morty utilizando el Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Función para rotar el personaje según la dirección del movimiento
    private void RotateCharacter()
    {
        if (movement.y > 0) // Movimiento hacia arriba
        {
            transform.eulerAngles = new Vector3(0, 0, upRotation);
        }
        else if (movement.y < 0) // Movimiento hacia abajo
        {
            transform.eulerAngles = new Vector3(0, 0, downRotation);
        }
        else if (movement.x < 0) // Movimiento hacia la izquierda
        {
            transform.eulerAngles = new Vector3(0, 0, leftRotation);
        }
        else if (movement.x > 0) // Movimiento hacia la derecha
        {
            transform.eulerAngles = new Vector3(0, 0, rightRotation);
        }
    }

    // Método para recoger un cerebro o llave
    private void CollectBrainOrKey()
    {
        // Lógica para recoger cerebros o llaves
        Debug.Log("Cerebro o llave recogida.");
        // Aquí podrías implementar la lógica específica para cada tipo de objeto.
    }

    // Método para recoger una pista
    private void CollectClue()
    {
        // Lógica para recoger una pista
        Debug.Log("Pista recogida.");
        // Implementar lógica específica para recoger pistas aquí.
    }
}
