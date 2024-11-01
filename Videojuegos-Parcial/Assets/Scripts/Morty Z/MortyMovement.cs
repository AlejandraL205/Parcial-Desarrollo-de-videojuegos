using System.Collections;
using UnityEngine;

public class MortyMovement : MonoBehaviour
{
    // Velocidad de movimiento
    public float moveSpeed = 5f; // Velocidad de movimiento del personaje
    private Rigidbody2D rb; // Referencia al componente Rigidbody2D
    private Vector2 movement; // Vector para almacenar la dirección de movimiento

    // Grados de rotación por dirección
    private float upRotation = 0f;       // Rotación hacia arriba
    private float downRotation = 180f;   // Rotación hacia abajo
    private float leftRotation = 270f;   // Rotación hacia la izquierda
    private float rightRotation = 90f;   // Rotación hacia la derecha

    void Start()
    {
        // Obtener la referencia al Rigidbody2D del Player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar el input del jugador
        movement.x = Input.GetAxisRaw("Horizontal"); // -1 para izquierda, 1 para derecha
        movement.y = Input.GetAxisRaw("Vertical");   // -1 para abajo, 1 para arriba

        // Normalizar el movimiento para mantener la misma velocidad en diagonal
        movement = movement.normalized;

        // Llamar a la función para rotar el personaje
        RotateCharacter();
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
}
