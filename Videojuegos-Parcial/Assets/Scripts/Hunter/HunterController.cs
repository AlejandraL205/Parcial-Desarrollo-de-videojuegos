using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float walkSpeed = 2f;               // Velocidad de caminar
    public float idleTime = 3f;                // Tiempo en reposo
    public float attackRadius = 5f;            // Rango de ataque
    public float detectionRadius = 10f;        // Rango de detección
    public float maxVerticalDistance = 3f;     // Máxima distancia de movimiento vertical
    public float maxHorizontalDistance = 3f;   // Máxima distancia de movimiento horizontal
    public bool moveVertically = false;        // Cambiar dirección de movimiento en el inspector

    private Animator animator;                 // Referencia al componente Animator
    private Transform player;                  // Referencia al jugador
    private bool isAttacking;                  // Estado de ataque
    private Vector3 originalPosition;          // Posición inicial
    private bool facingRight = true;           // Dirección de la mirada
    private Coroutine currentRoutine;          // Almacena la rutina activa

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;

        // Empezamos con el ciclo de caminar y reposo
        currentRoutine = StartCoroutine(WalkAndIdleRoutine());
    }

    private void Update()
    {
        // Detecta la distancia al jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            // Si el jugador está en rango de ataque y no estamos atacando, iniciamos ataque
            if (!isAttacking)
            {
                StartAttacking();
            }
        }
        else if (distanceToPlayer <= detectionRadius && !isAttacking)
        {
            // Si el jugador está en el rango de detección pero fuera de ataque, lo perseguimos
            if (currentRoutine != null)
            {
                StopCoroutine(currentRoutine);
            }
            currentRoutine = StartCoroutine(PursuePlayer());
        }
        else if (!isAttacking && currentRoutine == null)
        {
            // Si el jugador no está cerca y no estamos atacando, retomamos la rutina de caminar/reposo
            currentRoutine = StartCoroutine(WalkAndIdleRoutine());
        }
    }

    private IEnumerator WalkAndIdleRoutine()
    {
        while (true)
        {
            StartWalking();  // Empieza a caminar
            yield return new WaitForSeconds(idleTime);
            StopWalking();   // Pausa para reposar
            yield return new WaitForSeconds(idleTime);
        }
    }

    private void StartWalking()
    {
        animator.SetBool("isWalking", true);
        Debug.Log("Cazador caminando");

        // Inicia el movimiento en dirección vertical u horizontal
        if (moveVertically)
            currentRoutine = StartCoroutine(MoveVertically());
        else
            currentRoutine = StartCoroutine(MoveHorizontally());
    }

    private void StopWalking()
    {
        animator.SetBool("isWalking", false);
        Debug.Log("Cazador detenido");
    }

    private IEnumerator MoveVertically()
    {
        while (transform.position.y < originalPosition.y + maxVerticalDistance)
        {
            transform.position += Vector3.up * walkSpeed * Time.deltaTime;
            if (!facingRight) Flip(); // Voltea si es necesario
            yield return null;
        }

        StopWalking();
        yield return new WaitForSeconds(idleTime);

        while (transform.position.y > originalPosition.y)
        {
            transform.position -= Vector3.up * walkSpeed * Time.deltaTime;
            if (facingRight) Flip();
            yield return null;
        }

        StopWalking();
        yield return new WaitForSeconds(idleTime);

        StartWalking();
    }

    private IEnumerator MoveHorizontally()
    {
        while (transform.position.x < originalPosition.x + maxHorizontalDistance)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            if (!facingRight) Flip(); // Voltea si es necesario
            yield return null;
        }

        StopWalking();
        yield return new WaitForSeconds(idleTime);

        while (transform.position.x > originalPosition.x)
        {
            transform.position -= Vector3.right * walkSpeed * Time.deltaTime;
            if (facingRight) Flip();
            yield return null;
        }

        StopWalking();
        yield return new WaitForSeconds(idleTime);

        StartWalking();
    }

    private IEnumerator PursuePlayer()
    {
        animator.SetBool("isWalking", true);
        Debug.Log("Cazador persiguiendo al jugador");

        while (Vector3.Distance(transform.position, player.position) <= detectionRadius && !isAttacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * walkSpeed * Time.deltaTime;

            if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
                Flip(); // Voltea hacia el jugador si es necesario

            yield return null;
        }

        animator.SetBool("isWalking", false);
        Debug.Log("Cazador perdiendo de vista al jugador");

        currentRoutine = StartCoroutine(WalkAndIdleRoutine());
    }

    private void StartAttacking()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        Debug.Log("Cazador atacando al jugador");
    }

    private void StopAttacking()
    {
        isAttacking = false;
        Debug.Log("Cazador termina ataque");
    }

    public void OnAttackAnimationEnd()
    {
        StopAttacking();
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
            currentRoutine = StartCoroutine(WalkAndIdleRoutine());
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        Debug.Log("Cazador voltea de dirección");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado en área de visión del cazador");
        }
    }
}
