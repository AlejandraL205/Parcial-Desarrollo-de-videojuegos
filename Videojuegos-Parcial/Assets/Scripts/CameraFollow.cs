using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al jugador (Morty)
    public float smoothSpeed = 0.125f;  // Velocidad con la que se mover� la c�mara
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // Desplazamiento para la c�mara

    public Vector2 minBounds = new Vector2(-50f, -10f);  // L�mites del mapa
    public Vector2 maxBounds = new Vector2(50f, 20f);    // L�mites del mapa

    void FixedUpdate()
    {
        // Calculamos la posici�n deseada de la c�mara
        Vector3 desiredPosition = player.position + offset;

        // Restringimos la posici�n de la c�mara para que no se salga del mapa
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Movimiento suave hacia la posici�n deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Aseguramos que la c�mara siempre est� mirando al jugador
        transform.LookAt(player);
    }
}
