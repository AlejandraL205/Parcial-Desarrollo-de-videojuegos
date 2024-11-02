using System.Collections;
using UnityEngine;

public class HunterController : MonoBehaviour
{
    public float detectionRadius = 5f; // Radio de detecci�n para el jugador
    public float attackRadius = 1.5f; // Distancia para empezar a atacar
    public float walkSpeed = 2f;
    public float idleTime = 2f; // Tiempo que el cazador permanecer� quieto

    private Animator animator;
    private Transform player;
    private bool isWalking;
    private bool isAttacking;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(WalkAndIdleRoutine());
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRadius)
        {
            // Iniciar ataque si est� cerca del jugador
            if (!isAttacking)
            {
                StartAttacking();
            }
        }
        else if (distanceToPlayer <= detectionRadius)
        {
            // Caminar hacia el jugador si est� en el rango de detecci�n
            if (!isWalking && !isAttacking)
            {
                StartWalking();
            }
            MoveTowardsPlayer();
        }
        else
        {
            // Cambiar a Idle si el jugador no est� cerca
            if (isWalking)
            {
                StopWalking();
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * walkSpeed * Time.deltaTime;
    }

    private IEnumerator WalkAndIdleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(idleTime);
            if (!isAttacking)
            {
                isWalking = !isWalking;
                animator.SetBool("isWalking", isWalking);
            }
        }
    }

    private void StartWalking()
    {
        isWalking = true;
        animator.SetBool("isWalking", true);
    }

    private void StopWalking()
    {
        isWalking = false;
        animator.SetBool("isWalking", false);
    }

    private void StartAttacking()
    {
        isAttacking = true;
        isWalking = false;
        animator.SetBool("isWalking", false);
        animator.SetTrigger("Attack");
    }

    private void StopAttacking()
    {
        isAttacking = false;
        StartCoroutine(WalkAndIdleRoutine());
    }

    // M�todo llamado al final de la animaci�n de ataque
    public void OnAttackAnimationEnd()
    {
        StopAttacking();
    }
}
