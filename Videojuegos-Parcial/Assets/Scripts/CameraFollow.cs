using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Referencia al jugador (Morty)
    public float smoothSpeed = 0.125f;  // Velocidad con la que se moverá la cámara
    public Vector3 offset = new Vector3(0f, 5f, -10f);  // Desplazamiento para la cámara

    public Vector2 minBounds = new Vector2(-50f, -10f);  // Límites del mapa
    public Vector2 maxBounds = new Vector2(50f, 20f);    // Límites del mapa

    void FixedUpdate()
    {
        // Calculamos la posición deseada de la cámara
        Vector3 desiredPosition = player.position + offset;

        // Restringimos la posición de la cámara para que no se salga del mapa
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minBounds.x, maxBounds.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minBounds.y, maxBounds.y);

        // Movimiento suave hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Aseguramos que la cámara siempre esté mirando al jugador
        transform.LookAt(player);
    }
}
