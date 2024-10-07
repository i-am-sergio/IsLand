using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Asegúrate de incluir este espacio de nombres

public class CrabMovement : MonoBehaviour
{
    public Transform planeTransform; // Referencia al terreno donde el cangrejo puede moverse
    public float moveSpeed = 2.0f; // Velocidad de movimiento
    public float changeDirectionTime = 2.0f; // Tiempo entre cambios de dirección
    public AudioSource walkAudioSource; // Sonido de caminata

    private Vector3 movementDirection;
    private float timeSinceChange;
    private bool isImpaled = false; // Indica si el cangrejo ha sido clavado
    private bool isWalking = false; // Estado para verificar si el cangrejo está caminando
    private NavMeshAgent navMeshAgent; // Agente de navegación

    void Start()
    {
        // Obtener el componente NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Establecer propiedades del agente
        navMeshAgent.speed = moveSpeed;

        // Establecer una dirección aleatoria
        SetRandomDirection();
    }

    void Update()
    {
        if (!isImpaled)
        {
            MoveCrab();
        }
        else if (isWalking)
        {
            StopWalkingSound();
        }
    }

    void MoveCrab()
    {
        // Mueve el cangrejo usando el NavMeshAgent
        navMeshAgent.Move(movementDirection * moveSpeed * Time.deltaTime);

        // Cambiar de dirección después de un tiempo
        timeSinceChange += Time.deltaTime;
        if (timeSinceChange >= changeDirectionTime)
        {
            SetRandomDirection();
            timeSinceChange = 0;
        }
    }

    void SetRandomDirection()
    {
        // Establecer una dirección aleatoria
        movementDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        if (movementDirection == Vector3.zero && isWalking)
        {
            StopWalkingSound();
        }
        else
        {
            StartWalkingSound();
        }
    }

    void StartWalkingSound()
    {
        if (walkAudioSource != null && !walkAudioSource.isPlaying)
        {
            walkAudioSource.Play();
            isWalking = true;
        }
    }

    void StopWalkingSound()
    {
        if (walkAudioSource != null && walkAudioSource.isPlaying)
        {
            walkAudioSource.Stop();
            isWalking = false;
        }
    }

    public void ImpaleCrab()
    {
        isImpaled = true;
    }
}
