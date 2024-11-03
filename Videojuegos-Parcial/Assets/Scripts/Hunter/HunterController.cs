using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float walkSpeed = 2f; // Velocidad de caminar
    public float idleTime = 3f; // Tiempo que el cazador permanecerá quieto
    public float attackRadius = 5f; // Rango de ataque
    public float detectionRadius = 10f; // Rango de detección
    public float maxVerticalDistance = 3f; // Distancia máxima de movimiento vertical
    public float maxHorizontalDistance = 3f; // Distancia máxima de movimiento horizontal

    public bool moveVertically = false; // Opción para mover verticalmente

    private Animator animator; // Componente Animator del cazador
    private Transform player; // Referencia al jugador
    private bool isWalking; // Estado de movimiento del cazador
    private bool isAttacking; // Estado de ataque del cazador

    private Vector3 originalPosition; // Posición original del cazador

    private void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el componente Animator
        player = GameObject.FindGameObjectWithTag("Player").transform; // Busca el jugador por tag
        originalPosition = transform.position; // Guarda la posición original
        StartCoroutine(WalkAndIdleRoutine()); // Inicia la rutina de caminar e idle
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // Calcula la distancia al jugador

        // Si el jugador está dentro del rango de ataque
        if (distanceToPlayer <= attackRadius)
        {
            Debug.Log("Cazador en rango de ataque.");
            if (!isAttacking)
            {
                StartAttacking(); // Inicia el ataque
            }
        }
        else if (distanceToPlayer <= detectionRadius) // Si el jugador está dentro del rango de detección
        {
            Debug.Log("Cazador en rango de detección.");
            StopAllCoroutines(); // Detiene cualquier movimiento en curso
            StartCoroutine(PursuePlayer()); // Persigue al jugador
        }
    }

    private IEnumerator WalkAndIdleRoutine()
    {
        while (true)
        {
            if (!isAttacking)
            {
                StartWalking(); // Inicia el movimiento
                yield return new WaitForSeconds(idleTime); // Espera el tiempo de idle
                StopWalking(); // Detiene el movimiento
                yield return new WaitForSeconds(idleTime); // Espera antes de comenzar de nuevo
            }
            else
            {
                yield return null; // Esperar un frame si está atacando
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true; // Marca como que está caminando
        animator.SetBool("isWalking", true); // Cambia la animación a caminar
        Debug.Log("Cazador comenzando a caminar.");

        // Iniciar movimiento en la dirección especificada
        if (moveVertically)
        {
            StartCoroutine(MoveVertically());
        }
        else
        {
            StartCoroutine(MoveHorizontally());
        }
    }

    private void StopWalking()
    {
        isWalking = false; // Marca como que ha dejado de caminar
        animator.SetBool("isWalking", false); // Cambia la animación a idle
        Debug.Log("Cazador detenido.");
    }

    private IEnumerator MoveVertically()
    {
        // Camina hacia arriba
        while (transform.position.y < originalPosition.y + maxVerticalDistance)
        {
            transform.position += Vector3.up * walkSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, 0); // Mirar hacia arriba
            yield return null;
        }

        // Reposo en la posición superior
        StopWalking();
        yield return new WaitForSeconds(idleTime);

        // Regresa hacia abajo
        while (transform.position.y > originalPosition.y)
        {
            transform.position -= Vector3.up * walkSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 180, 0); // Mirar hacia abajo
            yield return null;
        }

        // Reposo en la posición original
        StopWalking();
        yield return new WaitForSeconds(idleTime);

        StartWalking(); // Reinicia el ciclo
    }

    private IEnumerator MoveHorizontally()
    {
        // Camina hacia la derecha
        while (transform.position.x < originalPosition.x + maxHorizontalDistance)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 90, 0); // Mirar hacia la derecha
            yield return null;
        }

        // Reposo en la posición derecha
        StopWalking();
        yield return new WaitForSeconds(idleTime);

        // Regresa hacia la izquierda
        while (transform.position.x > originalPosition.x)
        {
            transform.position -= Vector3.right * walkSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, -90, 0); // Mirar hacia la izquierda
            yield return null;
        }

        // Reposo en la posición original
        StopWalking();
        yield return new WaitForSeconds(idleTime);

        StartWalking(); // Reinicia el ciclo
    }

    private IEnumerator PursuePlayer()
    {
        while (Vector3.Distance(transform.position, player.position) <= detectionRadius)
        {
            Vector3 direction = (player.position - transform.position).normalized; // Dirección hacia el jugador
            transform.position += direction * walkSpeed * Time.deltaTime; // Mueve al cazador hacia el jugador

            // Ajusta la rotación para mirar hacia el jugador
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Debug.Log("Cazador persiguiendo al jugador.");
            yield return null;
        }

        // Vuelve a la rutina de caminar e idle
        StartCoroutine(WalkAndIdleRoutine());
    }

    private void StartAttacking()
    {
        isAttacking = true; // Marca como que está atacando
        StopWalking(); // Detiene el movimiento
        animator.SetTrigger("Attack"); // Cambia a la animación de ataque
        Debug.Log("Cazador comenzando a atacar.");
    }

    private void StopAttacking()
    {
        isAttacking = false; // Marca como que ha dejado de atacar
        StartCoroutine(WalkAndIdleRoutine()); // Regresa a la rutina de caminar e idle
    }

    // Método llamado al final de la animación de ataque
    public void OnAttackAnimationEnd()
    {
        StopAttacking(); // Finaliza el ataque
    }
}
