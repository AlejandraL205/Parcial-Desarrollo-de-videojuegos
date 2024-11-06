using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public int brainValue = 1;               // Valor de cerebros que el cazador da al ser derrotado
    public float walkSpeed = 2f;             // Velocidad de caminar
    public float idleTime = 3f;              // Tiempo en reposo
    public float attackRadius = 5f;          // Rango de ataque
    public float detectionRadius = 10f;      // Rango de detecci�n
    public bool moveVertically = false;      // Cambiar direcci�n de movimiento en el inspector
    private float stunnedTime = 2f;          // Tiempo de aturdimiento
    private float stunTimer = 0f;            // Temporizador de aturdimiento
    private bool isStunned = false;          // Estado de aturdimiento
    private bool isAttacking = false;        // Estado de ataque
    private Animator animator;               // Referencia al componente Animator
    private Transform player;                // Referencia al jugador
    private Vector3 originalPosition;        // Posici�n original
    private bool facingRight = true;         // Direcci�n de la mirada
    private Coroutine currentRoutine;        // Almacena la rutina activa

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;

        // Comienza el ciclo de caminar y reposar
        currentRoutine = StartCoroutine(WalkAndIdleRoutine());
    }

    private void Update()
    {
        // Si el cazador est� aturdido, no puede moverse ni atacar
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                Debug.Log("Cazador ya no est� aturdido.");
            }
            // Aqu� se puede hacer que el cazador se mueva m�s lento mientras est� aturdido
            walkSpeed = 0.5f;  // Reducimos la velocidad cuando est� aturdido
        }
        else
        {
            // Restauramos la velocidad normal
            walkSpeed = 2f;

            // Comportamiento normal cuando no est� aturdido
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRadius)
            {
                if (!isAttacking)
                {
                    StartAttacking();
                }
            }
            else if (distanceToPlayer <= detectionRadius && !isAttacking)
            {
                if (currentRoutine != null)
                {
                    StopCoroutine(currentRoutine);
                }
                currentRoutine = StartCoroutine(PursuePlayer());
            }
            else if (!isAttacking && currentRoutine == null)
            {
                currentRoutine = StartCoroutine(WalkAndIdleRoutine());
            }
        }
    }


    private IEnumerator WalkAndIdleRoutine()
    {
        while (true)
        {
            StartWalking();
            yield return new WaitForSeconds(idleTime);
            StopWalking();
            yield return new WaitForSeconds(idleTime);
        }
    }

    private void StartWalking()
    {
        animator.SetBool("isWalking", true);
        Debug.Log("Cazador caminando");

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
        while (transform.position.y < originalPosition.y + 3f)
        {
            transform.position += Vector3.up * walkSpeed * Time.deltaTime;
            if (!facingRight) Flip();
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
        while (transform.position.x < originalPosition.x + 3f)
        {
            transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            if (!facingRight) Flip();
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
                Flip();

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
        Debug.Log("Cazador voltea de direcci�n");
    }

    public void StunHunter()
    {
        isStunned = true;
        stunTimer = stunnedTime;
        Debug.Log("Cazador aturdido.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador detectado en �rea de visi�n del cazador");
            if (isStunned)
            {
                EatBrain();
            }
        }
    }

    private void EatBrain()
    {
        Debug.Log("Morty ha comido un cerebro.");
        // Aqu� puedes agregar la l�gica para sumar el valor del cerebro a la puntuaci�n o vida de Morty.
        MortyHealth mortyHealth = player.GetComponent<MortyHealth>();
        if (mortyHealth != null)
        {
            mortyHealth.AddLife(brainValue); // Ahora se llama a AddLife
            Debug.Log("Vida de Morty aumentada.");
        }
    }

}
